using Microsoft.AspNetCore.Mvc;

namespace CVCreationPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecommendationController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;

    public RecommendationController(ILogger<AuthController> logger)
        => (_logger) = (logger);

    [HttpPost("recommendations")]
    public Task<IActionResult> Recommend([FromQuery] string text)
    {
        throw new NotImplementedException();
    }

    [HttpPost("feedback/{resumeId}")]
    public Task<IActionResult> GiveFeedback([FromRoute] Guid resumeId)
    {
        throw new NotImplementedException();
    }
}