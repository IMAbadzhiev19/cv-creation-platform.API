using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.ResumeService.Models
{
    public class TemplateModel
    {
        [StringLength(40)]
        [Unicode(false)]
        public string? TemplateName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? FilePath { get; set; }
    }
}
