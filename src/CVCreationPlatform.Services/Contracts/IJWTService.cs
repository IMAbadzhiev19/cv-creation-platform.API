using CVCreationPlatform.AuthService.Models.Auth;

namespace CVCreationPlatform.AuthService.Contracts;

public interface IJWTService
{
    string CreateToken(LoginModel user);
}
