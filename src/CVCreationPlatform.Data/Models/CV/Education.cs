using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.CV;

public partial class Education
{
    [Key]
    public int Id { get; set; }

    public Guid? ResumeId { get; set; }

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
    public virtual Resume? Resume { get; set; }
}
