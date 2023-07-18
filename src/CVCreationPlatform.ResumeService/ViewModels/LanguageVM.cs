using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models;

public class LanguageVM
{
    public LanguageVM()
    {
        
    }

    public LanguageVM(Language language)
    {
        this.Id = language.Id;
        this.Name = language.Name;
        this.Level = language.Level;
    }
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Level { get; set; }
}
