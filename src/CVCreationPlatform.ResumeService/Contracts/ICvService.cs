using CVCreationPlatform.ResumeService.Models.DTO;
using CVCreationPlatform.ResumeService.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CVCreationPlatform.ResumeService.Contracts;

public interface ICvService
{
    Task<Guid> CreateResumeAsync(ResumeDTO resumeModel, string photoUrl);
    Task<bool> UpdateResumeAsync(Guid oldResumeId, ResumeDTO newResumeModel, string photoUrl);
    Task DeleteResumeAsync(Guid resumeId);
    Task<List<ResumeVM>> GetResumesByUserIdAsync(Guid userId);
    Task<ResumeVM> GetResumeByIdAsync(Guid resumeId);
    Task ShareResumeAsync(ShareDTO shareDto);
}