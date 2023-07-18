using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Contracts.BlobStorage;
using CVCreationPlatform.ResumeService.Models.DTO;
using CVCreationPlatform.ResumeService.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CVCreationPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]"), Authorize]
public class ResumeController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly ICvService _resumeService;
    private readonly IFileService _fileService;

    public ResumeController(ILogger<AuthController> logger, ICvService resumeService, IFileService fileService)
    => (_logger, _resumeService, _fileService) = (logger, resumeService, fileService);

    [HttpPost("resumes")]
    public async Task<IActionResult> CreateResume([FromForm] ResumeDTO resumeModel)
    {
        try
        {
            var personalInfo = resumeModel.PersonalInfo;
            string photoUrl = string.Empty;

            if (personalInfo != null)
                if (personalInfo.Photo != null)
                    photoUrl = await this._fileService.UploadImage(personalInfo.Photo);

            var id = await this._resumeService.CreateResumeAsync(resumeModel, photoUrl);
            return Ok($"Resume id: {id}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("resumes/{id}")]
    public async Task<ActionResult<ResumeVM>> GetResume([FromRoute] Guid id)
    {

        try
        {
            return Ok(await this._resumeService.GetResumeByIdAsync(id));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("resumes/{id}")]
    public async Task<IActionResult> UpdateResume([FromRoute] Guid id, [FromForm] ResumeDTO resumeModel)
    {
        try
        {
            var personalInfo = resumeModel.PersonalInfo;
            string photoUrl = string.Empty;

            if (personalInfo != null)
            {
                if (personalInfo.Photo != null)
                {
                    photoUrl = await this._fileService.UploadImage(personalInfo.Photo, id);
                }
            }

            await _resumeService.UpdateResumeAsync(id, resumeModel, photoUrl);
            return Ok($"The resume with id: {id} has been successfully updated");
        }
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

    [HttpDelete("resumes/{id}")]
    public async Task<IActionResult> DeleteResume([FromRoute] Guid id)
    {
        try
        {
            await this._resumeService.DeleteResumeAsync(id);
            return Ok($"The resume with id: {id} has been successfully deleted");
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("userResumes/{userId}")]
    public async Task<IActionResult> GetResumes([FromRoute] Guid userId)
    {
        try
        {
            return Ok(await this._resumeService.GetResumesByUserIdAsync(userId));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}