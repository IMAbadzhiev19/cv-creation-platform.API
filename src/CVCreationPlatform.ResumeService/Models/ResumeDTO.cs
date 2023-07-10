using Data.Models.CV;
using Data.Models.Auth;

namespace CVCreationPlatform.ResumeService.Models
{
    public class ResumeDTO
    {
        public int UserId { get; set; }

        public string? Title { get; set; }

        public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

        public virtual ICollection<Education> Educations { get; set; } = new List<Education>();

        public virtual ICollection<PersonalInfo> PersonalInfos { get; set; } = new List<PersonalInfo>();


        public virtual ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();

        public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

        public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
    }
}