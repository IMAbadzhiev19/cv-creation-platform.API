namespace CVCreationPlatform.AiService.Contracts;

public interface IAzureLanguageService
{
    Task<string> ExtractKeyPhrasesAsync(string text);
    Task<List<string>> SuggestSkillsAsync(string jobPositions);
}
