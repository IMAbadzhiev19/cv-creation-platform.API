using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVCreationPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]"), Authorize]
public class SkillController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly ISkillService _skillService;

    public SkillController(ILogger<AuthController> logger, ISkillService skillService)
    => (_logger, _skillService) = (logger, skillService);

    [HttpPost("skills/{resumeId}")]
    public async Task<IActionResult> AddSkillAsync([FromRoute] Guid resumeId, [FromForm] SkillDTO skillDTO)
    {
        try
        {
            await this._skillService.AssignSkillToResume(resumeId, skillDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("skills/{skillId}")]
    public async Task<IActionResult> UpdateSkillAsync([FromRoute] int skillId, [FromForm] SkillDTO newSkillDTO)
    {
        try
        {
            await this._skillService.UpdateSkill(skillId, newSkillDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("skills/{skillId}")]
    public async Task<IActionResult> DeleteSkillAsync([FromRoute] int skillId)
    {
        try
        {
            await this._skillService.DeleteSkill(skillId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}