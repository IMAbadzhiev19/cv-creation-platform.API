using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models;
using Data.Data;
using Data.Models.CV;
using Microsoft.EntityFrameworkCore;

namespace CVCreationPlatform.ResumeService.Implementations;

public class TemplateService : ITemplateService
{
    private readonly ApplicationDbContext _context;

    public TemplateService(ApplicationDbContext context)
        => _context = context;

    public async Task<bool> AddTemplate(TemplateDTO templateModel)
    {
        Template? template = null!;
        try
        {
            template = new Template()
            {
                TemplateName = templateModel.TemplateName,
                CssClassName = templateModel.CssClassName,
                FilePath = templateModel.FilePath
            };
            await _context.Templates.AddAsync(template);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ICollection<TemplateDTO>> GetTemplateModelsAsync()
       => await _context.Templates
        .Select(t => new TemplateDTO()
       {
           TemplateName = t.TemplateName,
           CssClassName = t.CssClassName,
           FilePath = t.FilePath
       }).ToListAsync();
}