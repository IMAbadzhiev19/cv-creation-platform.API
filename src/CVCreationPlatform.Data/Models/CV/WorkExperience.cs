using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.CV;

[Table("WorkExperience")]
public partial class WorkExperience
{
    [Key]
    public int Id { get; set; }

    public Guid? ResumeId { get; set; }

    [Unicode(false)]
    public string? CompanyName { get; set; }

    [Unicode(false)]
    public string? Position { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Unicode(false)]
    public string? Description { get; set; }

    [ForeignKey("ResumeId")]
    public virtual Resume? Resume { get; set; }
}
