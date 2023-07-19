using CVCreationPlatform.AuthService.Models.Auth;
using CVCreationPlatform.Data.Models.Auth;
using Data.Models.Auth;

namespace CVCreationPlatform.AuthService.Contracts;

public interface IJWTService
{
    Task<(string, DateTime)> CreateTokenAsync(LoginModel user);
	Task<RefreshToken> CreateRefreshTokenAsync();
	Task SetUserRefreshTokenAsync(string username, RefreshToken refreshToken);
	Task<bool> CheckUserRefreshtTokenValidity(string username);
	User GetUserRelatedToRefreshToken(string refreshToken);
}
