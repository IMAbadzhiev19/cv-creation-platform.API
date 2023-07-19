using Microsoft.AspNetCore.Http;

namespace CVCreationPlatform.ResumeService.Models.DTO;

public class ShareDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ReceiptantEmail { get; set; }
    public IFormFile? File { get; set; }
}
