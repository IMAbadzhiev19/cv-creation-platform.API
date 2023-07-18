using CVCreationPlatform.ResumeService.DTO;
using CVCreationPlatform.ResumeService.Models;

namespace CVCreationPlatform.ResumeService.Contracts;

public interface ICvService
{
    Task<Guid> CreateResumeAsync(ResumeDTO resumeModel, Guid id = default);
    Task<bool> UpdateResumeAsync(Guid oldResumeId, ResumeDTO newResumeModel);
    Task DeleteResumeAsync(Guid resumeId);
    Task<List<ResumeVM>> GetResumesByUserIdAsync(Guid userId);
    Task<ResumeVM> GetResumeByIdAsync(Guid resumeId);
}