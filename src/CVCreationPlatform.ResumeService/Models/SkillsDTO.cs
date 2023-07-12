namespace CVCreationPlatform.ResumeService.Models
{
	public class SkillsDTO
	{
		public SkillsDTO(string? skillName)
		{
			SkillName = skillName;

		}
		public string? SkillName { get; set; }
	}
}
