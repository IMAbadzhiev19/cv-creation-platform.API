using CVCreationPlatform.ResumeService.Models.DTO;

namespace CVCreationPlatform.ResumeService.Contracts;

public interface ISkillService
{
    Task<bool> AssignSkillToResume(Guid resumeId, SkillDTO skillDTO);
    Task<bool> UpdateSkill(int skillId, SkillDTO newSkillDTO);
    Task<bool> DeleteSkill(int skillId);
}