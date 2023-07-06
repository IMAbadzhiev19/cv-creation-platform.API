using CVCreationPlatform.Services.Contracts;
using CVCreationPlatform.Services.Models.Auth;
using Data.Data;
using Data.Models.Auth;
using System.Security.Cryptography;
using System.Text;

namespace CVCreationPlatform.Services.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task RegisterAsync(RegistrationModel registrationModel)
    {
        var user = new User()
        {
            Username = registrationModel.Username,
            Email = registrationModel.Email,
            Password = HashPassword(registrationModel.Password),
        };

        await this._context.AddAsync(user);
        await this._context.SaveChangesAsync();
    }
    public async Task<User> GetUserAsync(int id)
    {
        var user = await this._context.Users.FindAsync(id);
        
        if (user == null)
        {
            throw new ArgumentException("User with this id doesn't exist");
        }

        return user;
    }
    public async Task<bool> CheckLoginInformationAsync(LoginModel loginModel)
    {
        var user = this._context.Users.Where(u => u.Username == loginModel.Username).FirstOrDefault();
        
        if (user == null)
            throw new ArgumentException("User with this username does not exist");

        if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password))
            throw new ArgumentException("User with this password does not exist");

        return true;
    }

    private string HashPassword(string password)
    {
        string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(password);

        return passwordHash;
    }
}