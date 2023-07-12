using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CVCreationPlatform.Data.Models.CV;
using Data.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.CV;

public partial class Resume
{
    [Key]
    public Guid Id { get; set; }

    public int UserId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Title { get; set; }

    public PersonalInfo? PersonalInfo { get; set; }

    public UnknownSection? UnknownSection { get; set; }

    public Template? Template { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();

    public virtual ICollection<Language> Languages { get; set; } = new List<Language>();

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }

    public virtual ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();

    [ForeignKey("ResumeId")]
    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
