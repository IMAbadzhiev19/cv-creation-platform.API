using CVCreationPlatform.ResumeService.Models;

namespace CVCreationPlatform.ResumeService.Contracts;

public interface ITemplateService
{
    Task<ICollection<TemplateDTO>> GetTemplateModelsAsync();
    Task<bool> AddTemplate(TemplateDTO templateModel);
}