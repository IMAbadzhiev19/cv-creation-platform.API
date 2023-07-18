using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVCreationPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]"), Authorize]
public class EducationController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IEducationService _educationService;

    public EducationController(ILogger<AuthController> logger, IEducationService educationService)
    => (_logger, _educationService) = (logger, educationService);

    [HttpPost("educations/{resumeId}")]
    public async Task<IActionResult> AddEducationAsync([FromRoute] Guid resumeId, [FromForm] EducationDTO languageDto)
    {
        try
        {
            await this._educationService.AssignEducationToResume(resumeId, languageDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("educations/{educationId}")]
    public async Task<IActionResult> UpdateEducationAsync([FromRoute] int educationId, [FromForm] EducationDTO newLanguageDTO)
    {
        try
        {
            await this._educationService.UpdateEducation(educationId, newLanguageDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("educations/{educationId}")]
    public async Task<IActionResult> DeleteEducationAsync([FromRoute] int educationId)
    {
        try
        {
            await this._educationService.DeleteEducation(educationId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
