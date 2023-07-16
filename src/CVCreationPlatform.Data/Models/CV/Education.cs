using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.CV;

public partial class Education
{
    [Key]
    public int Id { get; set; }

    public Guid? ResumeId { get; set; }

    public string? InstituteName { get; set; }

    public string? Degree { get; set; }

    public string? FieldOfStudy { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [ForeignKey("ResumeId")]
    public virtual Resume? Resume { get; set; }
}
