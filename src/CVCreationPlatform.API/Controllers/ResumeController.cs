using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.DTO;
using CVCreationPlatform.ResumeService.Models;
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

            if (personalInfo != null)
            {
                if (personalInfo.Photo != null)
                {
                    var photoUrl = await this._fileService.UploadImage(personalInfo.Photo);
                    personalInfo.PhotoUrl = photoUrl;
                }
            }

            resumeModel = await this.GetRequestFormFieldsAsync(resumeModel);

            var id = await this._resumeService.CreateResumeAsync(resumeModel);
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
        resumeModel = await this.GetRequestFormFieldsAsync(resumeModel);
        try
        {
            var personalInfo = resumeModel.PersonalInfo;

            if (personalInfo != null)
            {
                if (personalInfo.Photo != null)
                {
                    personalInfo.PhotoUrl = await this._fileService.UploadImage(personalInfo.Photo, id);
                }
            }

            await _resumeService.UpdateResumeAsync(id, resumeModel);
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

    private async Task<ResumeDTO> GetRequestFormFieldsAsync(ResumeDTO resumeModel)
    {
        return await Task.Run(() =>
        {
            var certificatesJson = HttpContext.Request.Form["Certificates"].ToString();
            var educationsJson = HttpContext.Request.Form["Educations"].ToString();
            var workExperiencesJson = HttpContext.Request.Form["WorkExperiences"].ToString();
            var languagesJson = HttpContext.Request.Form["Languages"].ToString();
            var skillsJson = HttpContext.Request.Form["Skills"].ToString();

            if (!string.IsNullOrEmpty(certificatesJson))
            {
                certificatesJson = certificatesJson.Remove(0, 1).Insert(0, "[{");
                certificatesJson = certificatesJson.Remove(certificatesJson.Length - 1, 1).Insert(certificatesJson.Length - 1, "}]");
                var certificatesArray = JArray.Parse(certificatesJson);

                foreach (var cert in certificatesArray)
                {
                    var certificate = cert.ToObject<CertificateDTO>();
                    resumeModel.Certificates.Add(certificate);
                }
            }

            if (!string.IsNullOrEmpty(educationsJson))
            {
                educationsJson = educationsJson.Remove(0, 1).Insert(0, "[{");
                educationsJson = educationsJson.Remove(educationsJson.Length - 1, 1).Insert(educationsJson.Length - 1, "}]");
                var educationsArray = JArray.Parse(educationsJson);

                foreach (var educ in educationsArray)
                {
                    var education = educ.ToObject<EducationDTO>();
                    resumeModel.Educations.Add(education);
                }
            }

            if (!string.IsNullOrEmpty(workExperiencesJson))
            {
                workExperiencesJson = workExperiencesJson.Remove(0, 1).Insert(0, "[{");
                workExperiencesJson = workExperiencesJson.Remove(workExperiencesJson.Length - 1, 1).Insert(workExperiencesJson.Length - 1, "}]");
                var workExperiencesArray = JArray.Parse(workExperiencesJson);

                foreach (var workExp in workExperiencesArray)
                {
                    var workExperience = workExp.ToObject<WorkExperienceDTO>();
                    resumeModel.WorkExperiences.Add(workExperience);
                }
            }

            if (!string.IsNullOrEmpty(languagesJson))
            {
                languagesJson = languagesJson.Remove(0, 1).Insert(0, "[{");
                languagesJson = languagesJson.Remove(languagesJson.Length - 1, 1).Insert(languagesJson.Length - 1, "}]");
                var languageArray = JArray.Parse(languagesJson);

                foreach (var lang in languageArray)
                {
                    var language = lang.ToObject<LanguageDTO>();
                    resumeModel.Languages.Add(language);
                }
            }

            if (!string.IsNullOrEmpty(skillsJson))
            {
                skillsJson = skillsJson.Remove(0, 1).Insert(0, "[{");
                skillsJson = skillsJson.Remove(skillsJson.Length - 1, 1).Insert(skillsJson.Length - 1, "}]");
                var skillsArray = JArray.Parse(skillsJson);

                foreach (var skill in skillsArray)
                {
                    var skillDTO = skill.ToObject<SkillDTO>();
                    resumeModel.Skills.Add(skillDTO);
                }
            }

            return resumeModel;
        });
    }
}