using CVCreationPlatform.AiService.Models.ML;

namespace CVCreationPlatform.AiService.Contracts;

public interface IAzureCognitive
{
    Task<List<string>> GenerateSkillsForUser(string text);
}
