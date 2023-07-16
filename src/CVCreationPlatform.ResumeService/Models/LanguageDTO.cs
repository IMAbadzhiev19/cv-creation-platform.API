using Microsoft.EntityFrameworkCore;
using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models;

public class LanguageDTO
{
    public LanguageDTO()
    {
        
    }

    public LanguageDTO(Language language)
    {
        this.Name = language.Name;
        this.Level = language.Level;
    }

    public string? Name { get; set; }

    public string? Level { get; set; }
}
