using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.CV;

[Table("WorkExperience")]
public partial class WorkExperience
{
    [Key]
    public int Id { get; set; }

    public int? ResumeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CompanyName { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Position { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? Description { get; set; }

    [ForeignKey("ResumeId")]
    [InverseProperty("WorkExperiences")]
    public virtual Resume? Resume { get; set; }
}
