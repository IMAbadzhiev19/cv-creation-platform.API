using CVCreationPlatform.AuthService.Contracts;
using CVCreationPlatform.AuthService.Models.Auth;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CVCreationPlatform.AuthService.Implementations;

public class JWTService : IJWTService
{
    private readonly IConfiguration _configuration;
	private readonly ApplicationDbContext _context;

	public JWTService(ApplicationDbContext context, IConfiguration configuration)
	{
		_context = context;
		_configuration = configuration;
	}

    public async Task<string> CreateTokenAsync(LoginModel user)
    {
        return await Task.Run(() =>
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtSettings:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            /*var token = new JwtSecurityToken(
                _configuration.GetSection("JwtSettings:Issuer").Value,
                _configuration.GetSection("JwtSettings:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds);*/

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration.GetSection("JwtSettings:Issuer").Value,
                Audience = _configuration.GetSection("JwtSettings:Audience").Value,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = creds,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        });

    }
	public async Task<RefreshToken> CreateRefreshTokenAsync()
	{
		return await Task.Run(() =>
		{
			var refreshToken = new RefreshToken()
			{
				Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
				Created = DateTime.Now,
				Expires = DateTime.Now.AddDays(7),
			};
			return refreshToken;
		});
	}

	public async Task SetUserRefreshTokenAsync(string username, RefreshToken refreshToken)
	{
		var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
		if (user == null)
			throw new ArgumentException("Doesn't exist user with that username");
		user.Token = refreshToken.Token;
		user.TokenCreated = refreshToken.Created;
		user.TokenExpires = refreshToken.Expires;
		await _context.SaveChangesAsync();
	}

	public async Task<bool> CheckUserRefreshtTokenValidity(string username)
	{
		var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
		if (user == null)
			throw new ArgumentException("Doesn't exist user with that username");
		if (user.Token == null)
			return false;
		if (user.TokenExpires.HasValue && user.TokenExpires.Value < DateTime.UtcNow)
			return false;
		return true;
	}
}
