using CVCreationPlatform.AuthService.Models.Auth;

namespace CVCreationPlatform.AuthService.Contracts;

public interface IJWTService
{
    Task<string> CreateTokenAsync(LoginModel user);
}
