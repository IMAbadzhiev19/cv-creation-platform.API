using CVCreationPlatform.AuthService.Contracts;
using CVCreationPlatform.AuthService.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVCreationPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IUserService _userService;
    private readonly IJWTService _jWTService;

    public AuthController(ILogger<AuthController> logger, IUserService userService, IJWTService jwtService)
        => (_logger, _userService, _jWTService) = (logger, userService, jwtService);

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegistrationModel registrationModel)
    {
        try
        {
            await _userService.RegisterAsync(registrationModel);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("user/{id}"), Authorize]
    public async Task<IActionResult> GetUser(int id)
    {
        try
        {
            var user = await this._userService.GetUserAsync(id);
            user.RefreshToken = null;
            return Ok(user);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
    {
        try
        {
            bool result = this._userService.CheckLoginInformationAsync(loginModel);
			if (!await _jWTService.CheckUserRefreshtTokenValidity(loginModel.Username))
			{
				var refreshToken = await _jWTService.CreateRefreshTokenAsync();
                await _jWTService.SetUserRefreshTokenAsync(loginModel.Username, refreshToken);
            }

            var rf = await this._userService.GetRefreshTockenAsync(loginModel.Username);

            var returnedObj = new
            {
                JWT = await this._jWTService.CreateTokenAsync(loginModel),
                RefreshToken = rf.Token,
                RefreshTokenExpirationDate = rf.TokenExpires,
            };

			return Ok(returnedObj);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("logout/{userId}"), Authorize]
    public async Task<IActionResult> Logout(int userId)
    {
        try
        {
            var refreshToken = await this._userService.GetUserAsync(userId);
            var user = await this._userService.LogoutAsync(refreshToken.RefreshToken!.Token!);
            return Ok(user);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("renew-token")]
    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        try
        {
            var user = this._jWTService.GetUserRelatedToRefreshToken(refreshToken);

            var loginModel = new LoginModel
            {
                Username = user.Username!,
                Password = user.Password!,
            };

            return Ok(await this._jWTService.CreateTokenAsync(loginModel));
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }

    }
}