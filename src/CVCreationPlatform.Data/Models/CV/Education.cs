using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.CV;

public partial class Education
{
    [Key]
    public int Id { get; set; }

    public int? ResumeId { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? InstituteName { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Degree { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? FieldOfStudy { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [ForeignKey("ResumeId")]
    [InverseProperty("Educations")]
    public virtual Resume? Resume { get; set; }
}
