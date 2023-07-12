using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CVCreationPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize]
    public class ResumeController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ICvService _resumeService;

        public ResumeController(ILogger<AuthController> logger, ICvService resumeService)
        => (_logger, _resumeService) = (logger, resumeService);

        [HttpPost("resumes")]
        public async Task<IActionResult> CreateResume([FromForm] ResumeDTO resumeModel)
        {
            resumeModel = await this.GetRequestFormFieldsAsync(resumeModel);
            try
            {
                var id = await this._resumeService.CreateResumeAsync(resumeModel);
                return Ok($"Resume id: {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("resumes/{id}")]
        public async Task<IActionResult> GetResume([FromRoute] Guid id)
        {
            try
            {
                var result = await this._resumeService.GetResumeByIdAsync(id);
                var resumeObj = new
                {
                    Resume = result.Item1,
                    CreatedOn = result.Item2,
                    LastModified = result.Item3
                };

                return Ok(resumeObj);
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

        private async Task<ResumeDTO> GetRequestFormFieldsAsync(ResumeDTO resumeModel)
        {
            return await Task.Run(() =>
            {
                var certificatesJson = HttpContext.Request.Form["Certificates"];
                var educationsJson = HttpContext.Request.Form["Educations"];
                var workExperiencesJson = HttpContext.Request.Form["WorkExperiences"];
                var languagesJson = HttpContext.Request.Form["Languages"];
                var skillsJson = HttpContext.Request.Form["Skills"];

                if (certificatesJson.Count != 0)
                    foreach (var cert in JsonConvert.DeserializeObject<List<CertificateDTO>>(certificatesJson!)!)
                        resumeModel.Certificates.Add(cert);

                if (educationsJson.Count != 0)
                    foreach (var educ in JsonConvert.DeserializeObject<List<EducationDTO >> (educationsJson!)!)
                        resumeModel.Educations.Add(educ);

                if (workExperiencesJson.Count != 0)
                    foreach (var workExp in JsonConvert.DeserializeObject<List<WorkExperienceDTO>>(workExperiencesJson!)!)
                        resumeModel.WorkExperiences.Add(workExp);

                if (languagesJson.Count != 0)
                    foreach (var lang in JsonConvert.DeserializeObject<List<LanguageDTO>>(languagesJson!)!)
                        resumeModel.Languages.Add(lang);

                if (skillsJson.Count != 0)
                    foreach (var skill in JsonConvert.DeserializeObject<List<SkillsDTO>>(skillsJson!)!)
                        resumeModel.Skills.Add(skill);

                return resumeModel;
            });
        }
    }
}
