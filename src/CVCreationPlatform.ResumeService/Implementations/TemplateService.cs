using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models;
using Data.Data;

namespace CVCreationPlatform.ResumeService.Implementations
{
    public class TemplateService : ITemplateService
    { 
        private readonly ApplicationDbContext _context;

        public TemplateService(ApplicationDbContext context)
            => _context = context;

        public Task<bool> AddTemplate(TemplateModel templateModel)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TemplateModel>> GetTemplateModelsAsync()
        {
            throw new NotImplementedException();
        }
    }
}