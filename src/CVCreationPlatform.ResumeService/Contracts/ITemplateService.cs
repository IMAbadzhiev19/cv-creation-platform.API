using CVCreationPlatform.ResumeService.Models;

namespace CVCreationPlatform.ResumeService.Contracts
{
    public interface ITemplateService
    {
        Task<ICollection<TemplateModel>> GetTemplateModelsAsync();
        Task<bool> AddTemplate(TemplateModel templateModel);
    }
}