using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
            var id = await this._resumeService.CreateResumeAsync(resumeModel);
            return Ok($"Resume id: {id}");
        }

        [HttpGet("templates")]
        public async Task<IActionResult> GetTemplates()
        {
            throw new NotImplementedException();
        }
    }
}
