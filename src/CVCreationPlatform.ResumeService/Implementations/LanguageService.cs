using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models.DTO;
using Data.Data;
using Data.Models.CV;
using Microsoft.EntityFrameworkCore;

namespace CVCreationPlatform.ResumeService.Implementations;

public class LanguageService : ILanguageService
{
    private readonly ApplicationDbContext _context;

    public LanguageService(ApplicationDbContext context)
        => _context = context;

    public async Task<bool> AssignLanguageToResume(Guid resumeId, LanguageDTO languageDTO)
    {
        var resume = await _context.Resumes.FirstOrDefaultAsync(r => r.Id == resumeId);
        if (resume == null)
            throw new ArgumentException("Invalid language id");

        var languageToAdd = new Language
        {
            ResumeId = resume.Id,
            Resume = resume,
            Name = languageDTO.Name,
            Level = languageDTO.Level
        };

        resume.Languages.Add(languageToAdd);

        await this._context.Languages.AddAsync(languageToAdd);
        await this._context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateLanguage(int languageId, LanguageDTO newLanguageDTO)
    {
        var language = await _context.Languages.FindAsync(languageId);
        if (language == null)
            throw new ArgumentException("Invalid language id");

        language.Name = newLanguageDTO.Name;
        language.Level = newLanguageDTO.Level;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteLanguage(int languageId)
    {
        var languageToRemove = await _context.Languages.FindAsync(languageId);
        if (languageToRemove == null)
            throw new ArgumentException("Invalid language id");

        _context.Languages.Remove(languageToRemove);
        await this._context.SaveChangesAsync();
        return true;
    }
}
