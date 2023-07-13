using Data.Models.CV;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.ResumeService.Models;

public class TemplateDTO
{
    public TemplateDTO()
    {
        
    }
    public TemplateDTO(Template template)
    {
        if(template != null)
        {
            this.TemplateName = template.TemplateName;
            this.CssClassName = template.CssClassName;
            this.FilePath = template.FilePath;
        }
    }

    [Unicode(false)]
    public string? TemplateName { get; set; }

    [Unicode(false)]
    public string? CssClassName { get; set; }

    [Unicode(false)]
    public string? FilePath { get; set; }
}
