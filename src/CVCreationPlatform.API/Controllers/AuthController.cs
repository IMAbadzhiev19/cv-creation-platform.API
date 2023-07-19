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

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login([FromForm] LoginModel loginModel)
    {
        try
        {
            var user = this._userService.CheckLoginInformationAsync(loginModel);
			if (!await _jWTService.CheckUserRefreshtTokenValidity(loginModel.Username))
			{
				var refreshToken = await _jWTService.CreateRefreshTokenAsync();
                await _jWTService.SetUserRefreshTokenAsync(loginModel.Username, refreshToken);
            }

            var rf = await this._userService.GetRefreshTockenAsync(loginModel.Username);
            var jwtOptions = await this._jWTService.CreateTokenAsync(loginModel);

            var userToReturn = new UserDTO()
            {
                Id = user.Id,
                Username = user.Username,
                Jwt = jwtOptions.Item1,
                JWTExpirationDate = jwtOptions.Item2,
                RefreshToken = rf.Token,
                RefreshTokenExpirationDate = rf.TokenExpires,
            };

            return Ok(userToReturn);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("logout/{userId}"), Authorize]
    public async Task<IActionResult> Logout([FromRoute] Guid userId)
    {
        try
        {
            var refreshToken = await this._userService.GetUserAsync(userId);
            var user = await this._userService.LogoutAsync(refreshToken.RefreshToken!.Token!);
            return Ok();
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("renew-token")]
    public async Task<IActionResult> RefreshToken([FromForm] string refreshToken)
    {
        try
        {
            var user = this._jWTService.GetUserRelatedToRefreshToken(refreshToken);

            var loginModel = new LoginModel
            {
                Username = user.Username!,
                Password = user.Password!,
            };

            var jwtOptions = await this._jWTService.CreateTokenAsync(loginModel);

            return Ok(new
            {
                Jwt = jwtOptions.Item1,
                JwtExpirationDate = jwtOptions.Item2
            });
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }

    }
}