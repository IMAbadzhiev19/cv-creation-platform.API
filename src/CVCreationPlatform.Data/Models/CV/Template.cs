using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.CV;

public partial class Template
{
    [Key]
    public int Id { get; set; }

    public string? TemplateName { get; set; }

    [ForeignKey("TemplateId")]
    public virtual ICollection<Resume> Resumes { get; set; } = new List<Resume>();
}
