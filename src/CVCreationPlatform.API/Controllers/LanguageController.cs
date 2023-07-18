using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVCreationPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]"), Authorize]
public class LanguageController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly ILanguageService _languageService;

    public LanguageController(ILogger<AuthController> logger, ILanguageService languageService)
    => (_logger, _languageService) = (logger, languageService);

    [HttpPost("languages/{resumeId}")]
    public async Task<IActionResult> AddLanguageAsync([FromRoute] Guid resumeId, [FromForm] LanguageDTO languageDto)
    {
        try
        {
            await this._languageService.AssignLanguageToResume(resumeId, languageDto);
            return Ok();
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("languages/{languageId}")]
    public async Task<IActionResult> UpdateLanguageAsync([FromRoute] int languageId, [FromForm] LanguageDTO newLanguageDTO)
    {
        try
        {
            await this._languageService.UpdateLanguage(languageId, newLanguageDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("languages/{languageId}")]
    public async Task<IActionResult> DeleteLanguageAsync([FromRoute] int languageId)
    {
        try
        {
            await this._languageService.DeleteLanguage(languageId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
