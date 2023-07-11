using CVCreationPlatform.ResumeService.Models;

namespace CVCreationPlatform.ResumeService.Contracts
{
    public interface ICvService
    {
        Task<Guid> CreateResumeAsync(ResumeDTO resumeModel);
        Task<bool> UpdateResumeAsync(Guid oldResumeId, ResumeDTO newResumeModel);
        Task DeleteResumeAsync(Guid resumeId);
        Task<(ResumeDTO, DateTime, DateTime)> GetResumeByIdAsync(Guid resumeId);
    }
}