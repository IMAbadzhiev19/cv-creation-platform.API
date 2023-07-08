using CVCreationPlatform.AuthService.Models.Auth;

namespace CVCreationPlatform.AuthService.Contracts;

public interface IJWTService
{
    Task<string> CreateTokenAsync(LoginModel user);
	Task<RefreshToken> CreateRefreshTokenAsync();
	Task SetUserRefreshTokenAsync(string username, RefreshToken refreshToken);
	Task<bool> CheckUserRefreshtTokenValidity(string username);
}
