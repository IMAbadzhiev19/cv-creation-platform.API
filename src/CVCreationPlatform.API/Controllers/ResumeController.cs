using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return default;
        }

        [HttpGet("templates")]
        public async Task<IActionResult> GetTemplates()
        {
            return default;
        }
    }
}
