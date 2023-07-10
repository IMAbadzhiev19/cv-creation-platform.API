using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.CV;

public partial class Template
{
    [Key]
    public int Id { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? TemplateName { get; set; }

    [Unicode(false)]
    public string? CssClassName { get; set;}

    [StringLength(100)]
    [Unicode(false)]
    public string? FilePath { get; set; }
}
