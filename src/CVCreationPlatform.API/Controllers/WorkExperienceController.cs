using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVCreationPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]"), Authorize]
public class WorkExperienceController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IWorkExperienceService _workExperience;

    public WorkExperienceController(ILogger<AuthController> logger, IWorkExperienceService skillService)
    => (_logger, _workExperience) = (logger, skillService);

    [HttpPost("workExperiences/{resumeId}")]
    public async Task<IActionResult> AddWorkExperienceAsync([FromRoute] Guid resumeId, [FromForm] WorkExperienceDTO workExperienceDTO)
    {
        try
        {
            await this._workExperience.AssignWorkExperienceToResume(resumeId, workExperienceDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("workExperiences/{workExperienceId}")]
    public async Task<IActionResult> UpdateWorkExperienceAsync([FromRoute] int workExperienceId, [FromForm] WorkExperienceDTO newWorkExperienceDTO)
    {
        try
        {
            await this._workExperience.UpdateWorkExperience(workExperienceId, newWorkExperienceDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("workExperiences/{workExperienceId}")]
    public async Task<IActionResult> DeleteWorkExperienceAsync([FromRoute] int workExperienceId)
    {
        try
        {
            await this._workExperience.DeleteSkill(workExperienceId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}