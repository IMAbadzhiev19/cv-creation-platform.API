using Microsoft.AspNetCore.Http;

namespace CVCreationPlatform.ResumeService.Contracts;

public interface IFileService
{
    public Task<string> UploadImage(IFormFile imageFile, Guid resumeId = default);
}
