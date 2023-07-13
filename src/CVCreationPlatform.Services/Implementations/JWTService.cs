using CVCreationPlatform.AuthService.Contracts;
using CVCreationPlatform.AuthService.Models.Auth;
using CVCreationPlatform.Data.Models.Auth;
using Data.Data;
using Data.Models.Auth;
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
                Expires = DateTime.UtcNow.AddMinutes(70),
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
                TokenCreated = DateTime.UtcNow,
                TokenExpires = DateTime.UtcNow.AddDays(7),
			};
			return refreshToken;
		});
	}
	public async Task SetUserRefreshTokenAsync(string username, RefreshToken refreshToken)
	{
		var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
		if (user == null)
			throw new ArgumentException("Doesn't exist user with that username");

		user.RefreshToken = refreshToken;

		this._context.RefreshTokens.Add(refreshToken);
		this._context.Users.Update(user);
		await _context.SaveChangesAsync();
	}
	public async Task<bool> CheckUserRefreshtTokenValidity(string username)
	{
		var user = await _context.Users.Include(u => u.RefreshToken).FirstOrDefaultAsync(u => u.Username == username);
		if (user == null)
			throw new ArgumentException("Doesn't exist user with that username");
		if (user.RefreshToken == null)
			return false;
		if (user.RefreshToken.Token == null)
			return false;
		if (user.RefreshToken!.TokenExpires.HasValue && user.RefreshToken!.TokenExpires.Value < DateTime.UtcNow)
			return false;

		return true;
	}
	public User GetUserRelatedToRefreshToken(string refreshToken)
	{
        var user = _context.Users.Include(u => u.RefreshToken).FirstOrDefault(u => u.RefreshToken!.Token == refreshToken)!;

		if (user == null)
			throw new ArgumentException("You should login in order for a new refresh token to be created.\r\n");

        return user;
	}
}