namespace CVCreationPlatform.AiService.Models.Resume;

public class SkillDTO
{
    public SkillDTO(string? skillName)
    {
        SkillName = skillName;

    }
    public string? SkillName { get; set; }
}