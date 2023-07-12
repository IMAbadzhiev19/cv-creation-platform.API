using CVCreationPlatform.ResumeService.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVCreationPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize]
    public class TemplateController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ITemplateService _templateService;

        public TemplateController(ILogger<AuthController> logger, ITemplateService templateService)
        => (_logger, _templateService) = (logger, templateService);

        [HttpGet("templates")]
        public async Task<IActionResult> GetTemplates()
        {
            try
            {
                return Ok(await this._templateService.GetTemplateModelsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
