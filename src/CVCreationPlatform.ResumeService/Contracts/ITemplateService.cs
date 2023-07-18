using CVCreationPlatform.ResumeService.DTO;
using CVCreationPlatform.ResumeService.Models;

namespace CVCreationPlatform.ResumeService.Contracts;

public interface ITemplateService
{
    Task<ICollection<TemplateVM>> GetTemplateModelsAsync();
    Task<bool> AddTemplate(TemplateDTO templateModel);
}