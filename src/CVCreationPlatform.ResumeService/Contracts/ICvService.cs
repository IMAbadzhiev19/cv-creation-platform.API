using CVCreationPlatform.ResumeService.Models;

namespace CVCreationPlatform.ResumeService.Contracts;

public interface ICvService
{
    Task<Guid> CreateResumeAsync(ResumeDTO resumeModel, Guid id = default);
    Task<bool> UpdateResumeAsync(Guid oldResumeId, ResumeDTO newResumeModel);
    Task DeleteResumeAsync(Guid resumeId);
    Task<List<ResumeDTO>> GetResumesByUserIdAsync(int userId);
    Task<ResumeDTO> GetResumeByIdAsync(Guid resumeId);
}