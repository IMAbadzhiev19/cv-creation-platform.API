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
            return Ok(); //We can returns a JWT Tocken here
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
				await SetRefreshTokenAsync(loginModel.Username, refreshToken);
			}
			return Ok(await this._jWTService.CreateTokenAsync(loginModel));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
	private async Task SetRefreshTokenAsync(string username, RefreshToken refreshToken)
	{
		var cookieOptions = new CookieOptions()
		{
			HttpOnly = true,
			Expires = refreshToken.Expires
		};
		Response.Cookies.Append("refreshtoken", refreshToken.Token, cookieOptions);
		await _jWTService.SetUserRefreshTokenAsync(username, refreshToken);
	}
}