using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models;

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
            this.TemplateName = template.TemplateName;
        }
    }
    public int Id { get; set; }
    public string? TemplateName { get; set; }
}
