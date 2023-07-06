using CVCreationPlatform.Services.Models.Auth;
using Data.Models.Auth;

namespace CVCreationPlatform.Services.Contracts;

public interface IUserService
{
    Task RegisterAsync(RegistrationModel registrationModel);
    Task<User> GetUserAsync(int id);
    Task<bool> CheckLoginInformationAsync(LoginModel loginModel);
}