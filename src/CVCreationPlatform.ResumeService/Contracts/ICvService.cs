using CVCreationPlatform.ResumeService.Models;

namespace CVCreationPlatform.ResumeService.Contracts
{
    public interface ICvService
    {
        Task<bool> CreateResumeAsync(ResumeDTO resumeModel);
        Task<bool> UpdateResumeAsync(int oldResumeId, ResumeDTO newResumeModel);
        Task<bool> DeleteResumeAsync(int resumeId);
        Task<ResumeDTO> GetResumeByIdAsync(int resumeId);
    }
}