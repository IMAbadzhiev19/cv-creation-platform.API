using CVCreationPlatform.Services.Models.Auth;
using CVCreationPlatform.Services.Services;
using Data.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CVCreationPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserService _userService;

    public UserController(ILogger<UserController> logger, UserService userService)
        => (_logger, _userService) = (logger, userService);

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationModel registrationModel)
    {
        try
        {
            Console.WriteLine(registrationModel.FirstName);
            await _userService.CreateUserAsync(registrationModel);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
