using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models.ViewModels;

public class LanguageVM
{
    public LanguageVM()
    {

    }

    public LanguageVM(Language language)
    {
        Id = language.Id;
        Name = language.Name;
        Level = language.Level;
    }
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Level { get; set; }
}
