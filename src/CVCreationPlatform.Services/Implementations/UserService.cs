using CVCreationPlatform.AuthService.Contracts;
using CVCreationPlatform.AuthService.Models.Auth;
using CVCreationPlatform.Data.Models.Auth;
using Data.Data;
using Data.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace CVCreationPlatform.AuthService.Implementations;

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
            Id = Guid.NewGuid(),
            Username = registrationModel.Username,
            Email = registrationModel.Email,
            Password = await HashPasswordAsync(registrationModel.Password),
        };

        await this._context.AddAsync(user);
        await this._context.SaveChangesAsync();
    }
    public async Task<User> GetUserAsync(Guid id)
    {
        var user = await this._context.Users.Include(u => u.RefreshToken).FirstOrDefaultAsync(ui => ui.Id == id);

        if (user == null)
        {
            throw new ArgumentException("User with this id doesn't exist");
        }

        return user;
    }
    public User CheckLoginInformationAsync(LoginModel loginModel)
    {
        var user = this._context.Users.FirstOrDefault(u => u.Username == loginModel.Username);

        if (user == null)
            throw new ArgumentException("User with this username does not exist");

        if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password))
            throw new ArgumentException("User with this password does not exist");

        return user;
    }
    public async Task<User> LogoutAsync(string refreshToken)
        => await this.DeleteRefreshTokenAsync(refreshToken);

    public async Task<RefreshToken> GetRefreshTockenAsync(string username)
    {
        var user = await this._context.Users
            .Include(u => u.RefreshToken)
            .FirstOrDefaultAsync(un => un.Username == username);

        return user!.RefreshToken!;
    }

    private async Task<string> HashPasswordAsync(string password)
    {
        return await Task.Run(() =>
        {
            string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(password);

            return passwordHash;
        });
    }
    private async Task<User> DeleteRefreshTokenAsync(string refreshToken)
    {
        return await Task.Run(() =>
        {
            var rf = this._context.RefreshTokens.Where(x => x.Token == refreshToken).FirstOrDefault();
            if (rf == default)
                throw new ArgumentException("Invalid refresh token");

            var user = this._context.Users.Where(u => u.RefreshTokenId == rf.Id).FirstOrDefault();
            if (user == null)
                throw new ArgumentException("There is no user related to this refresh token");

            user.RefreshTokenId = null;
            user.RefreshToken = null;

            this._context.Users.Update(user);
            this._context.RefreshTokens.Remove(rf);
            this._context.SaveChanges();

            return rf.User!;
        });
    }
}