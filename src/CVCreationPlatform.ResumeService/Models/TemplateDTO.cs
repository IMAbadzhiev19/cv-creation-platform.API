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
        }
    }

    [Unicode(false)]
    public string? TemplateName { get; set; }
}
