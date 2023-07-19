using CVCreationPlatform.AiService.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVCreationPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]"), Authorize]
public class RecommendationController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAzureLanguageService _languageService;

    public RecommendationController(ILogger<AuthController> logger, IAzureLanguageService languageService)
        => (_logger, _languageService) = (logger, languageService);

    [HttpPost("recommendations")]
    public async Task<IActionResult> Recommend([FromForm] string text)
    {
        try
        {
            var jobPositions = await this._languageService.ExtractKeyPhrasesAsync(text);
            return Ok(await this._languageService.SuggestSkillsAsync(jobPositions));
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("feedback/{resumeId}")]
    public async Task<IActionResult> GiveFeedback([FromRoute] Guid resumeId)
    {
        throw new NotImplementedException();
    }
}