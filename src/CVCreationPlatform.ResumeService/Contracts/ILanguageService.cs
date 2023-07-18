using CVCreationPlatform.ResumeService.Models.DTO;

namespace CVCreationPlatform.ResumeService.Contracts;

public interface ILanguageService
{
    Task<bool> AssignLanguageToResume(Guid resumeId, LanguageDTO languageDTO);
    Task<bool> UpdateLanguage(int languageId, LanguageDTO newLanguageDTO);
    Task<bool> DeleteLanguage(int languageId);
}
