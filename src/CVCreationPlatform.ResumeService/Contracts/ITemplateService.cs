using CVCreationPlatform.ResumeService.Models.ViewModels;

namespace CVCreationPlatform.ResumeService.Contracts;

public interface ITemplateService
{
    Task<ICollection<TemplateVM>> GetTemplateModelsAsync();
}