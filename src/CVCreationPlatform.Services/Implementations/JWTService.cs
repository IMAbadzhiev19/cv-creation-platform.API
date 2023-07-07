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

    public string CreateToken(LoginModel user)
    {
        List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
            };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        Console.WriteLine(key);

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}
