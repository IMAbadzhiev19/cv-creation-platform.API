using CVCreationPlatform.AuthService.Contracts;
using CVCreationPlatform.AuthService.Models.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CVCreationPlatform.AuthService.Implementations;

public class JWTService : IJWTService
{
    private readonly IConfiguration _configuration;

    public JWTService(IConfiguration configuration)
        => _configuration = configuration;

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
}
