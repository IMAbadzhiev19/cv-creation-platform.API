using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVCreationPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]"), Authorize]
public class CertificateController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly ICertificateService _certificateService;

    public CertificateController(ILogger<AuthController> logger, ICertificateService certificateService)
    => (_logger, _certificateService) = (logger, certificateService);

    [HttpPost("certificates/{resumeId}")]
    public async Task<IActionResult> AddCertificateAsync([FromRoute] Guid resumeId, [FromForm] CertificateDTO certificateDTO)
    {
        try
        {
            await this._certificateService.AssignCertificateToResume(resumeId, certificateDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("certificates/{certificateId}")]
    public async Task<IActionResult> UpdateCertificateAsync([FromRoute] int certificateId, [FromForm] CertificateDTO newCertificateDTO)
    {
        try
        {
            await this._certificateService.UpdateCertificate(certificateId, newCertificateDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("certificates/{certificateId}")]
    public async Task<IActionResult> DeleteCertificateAsync([FromRoute] int certificateId)
    {
        try
        {
            await this._certificateService.DeleteCertificate(certificateId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
