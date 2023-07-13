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

    public string? PhotoUrl { get; set; }

    public Guid? ResumeId { get; set; }

    [Unicode(false)]
    public string? FullName { get; set; }
    [Unicode(false)]
    public string? Address { get; set; }

    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [Unicode(false)]
    public string? Email { get; set; }

    [ForeignKey("ResumeId")]
    public virtual Resume? Resume { get; set; }
}
