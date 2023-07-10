using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.CV;

[Table("PersonalInfo")]
public partial class PersonalInfo
{
    [Key]
    public int Id { get; set; }

    [StringLength(2058)]
    string? PhotoUrl { get; set; }

    public int? ResumeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FullName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Address { get; set; }

    [StringLength(13)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [ForeignKey("ResumeId")]
    [InverseProperty("PersonalInfos")]
    public virtual Resume? Resume { get; set; }
}
