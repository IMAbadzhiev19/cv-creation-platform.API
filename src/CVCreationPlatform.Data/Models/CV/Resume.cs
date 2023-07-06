using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.CV;

public partial class Resume
{
    [Key]
    public int Id { get; set; }

    public int? UserId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Title { get; set; }

    public DateTime? CreationDate { get; set; }

    [Column("Last_Modified_Date")]
    public DateTime? LastModifiedDate { get; set; }

    [InverseProperty("Resume")]
    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    [InverseProperty("Resume")]
    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();

    [InverseProperty("Resume")]
    public virtual ICollection<PersonalInfo> PersonalInfos { get; set; } = new List<PersonalInfo>();

    [ForeignKey("UserId")]
    [InverseProperty("Resumes")]
    public virtual User? User { get; set; }

    [InverseProperty("Resume")]
    public virtual ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();

    [ForeignKey("ResumeId")]
    [InverseProperty("Resumes")]
    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    [ForeignKey("ResumeId")]
    [InverseProperty("Resumes")]
    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
