using CVCreationPlatform.ResumeService.Models.DTO;

namespace CVCreationPlatform.ResumeService.Contracts;

public interface IWorkExperienceService
{
    Task<bool> AssignWorkExperienceToResume(Guid resumeId, WorkExperienceDTO workExperienceDTO);
    Task<bool> UpdateWorkExperience(int workExperienceId, WorkExperienceDTO newWorkExperienceDTO);
    Task<bool> DeleteSkill(int workExperienceId);
}
