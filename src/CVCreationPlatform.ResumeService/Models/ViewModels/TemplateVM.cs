using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models.ViewModels;

public class TemplateVM
{
    public TemplateVM()
    {

    }
    public TemplateVM(Template? template)
    {
        if (template != null)
        {
            Id = template.Id;
            TemplateName = template.TemplateName;
        }
    }
    public int Id { get; set; }
    public string? TemplateName { get; set; }
}
