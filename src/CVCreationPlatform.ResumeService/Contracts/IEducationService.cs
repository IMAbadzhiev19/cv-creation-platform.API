using CVCreationPlatform.ResumeService.Models.DTO;

namespace CVCreationPlatform.ResumeService.Contracts;

public interface IEducationService
{
    Task<bool> AssignEducationToResume(Guid resumeId, EducationDTO educationDTO);
    Task<bool> UpdateEducation(int educationId, EducationDTO newEducationDTO);
    Task<bool> DeleteEducation(int educationId);
}
