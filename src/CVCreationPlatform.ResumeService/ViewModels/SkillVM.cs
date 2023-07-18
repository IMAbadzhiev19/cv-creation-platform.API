using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models;

public class SkillVM
{
	public SkillVM(Skill skill)
	{
		Id = skill.Id;
		SkillName = skill.SkillName;
	}
    public int Id { get; set; }
    public string? SkillName { get; set; }
}
