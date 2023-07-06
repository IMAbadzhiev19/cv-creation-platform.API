﻿using CVCreationPlatform.Services.Contracts;
using CVCreationPlatform.Services.Models.Auth;
using Data.Data;
using Data.Models.Auth;
using Data.Models.CV;
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

    public async Task CreateUserAsync(RegistrationModel registrationModel)
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
    public async Task<User> GetUserAsync(Guid id)
    {
        var user = await this._context.Users.FindAsync(id);
        
        if (user == null)
        {
            throw new ArgumentException("User with this id doesn't exist");
        }

        return user;
    }

    public string HashPassword(string password)
    {
        byte[] passBytes = Encoding.Unicode.GetBytes(password);
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(passBytes);
            string shaString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return shaString;
        }
    }
}