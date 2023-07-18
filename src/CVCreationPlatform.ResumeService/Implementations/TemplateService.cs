using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models.ViewModels;
using Data.Data;
using Data.Models.CV;
using Microsoft.EntityFrameworkCore;

namespace CVCreationPlatform.ResumeService.Implementations;

public class TemplateService : ITemplateService
{
    private readonly ApplicationDbContext _context;

    public TemplateService(ApplicationDbContext context)
        => _context = context;

    public async Task<ICollection<TemplateVM>> GetTemplateModelsAsync()
       => await _context.Templates
        .Where(t => t.TemplateName != null)
        .Select(t => new TemplateVM(t))
        .ToListAsync();
}