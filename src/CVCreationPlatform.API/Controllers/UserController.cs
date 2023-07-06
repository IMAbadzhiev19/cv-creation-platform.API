using CVCreationPlatform.Services.Contracts;
using CVCreationPlatform.Services.Models.Auth;
using CVCreationPlatform.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace CVCreationPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, UserService userService)
        => (_logger, _userService) = (logger, userService);

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationModel registrationModel)
    {
        try
        {
            await _userService.CreateUserAsync(registrationModel);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("user/{id}")]
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
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        try
        {
            bool result = await this._userService.CheckLoginInformationAsync(loginModel);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
