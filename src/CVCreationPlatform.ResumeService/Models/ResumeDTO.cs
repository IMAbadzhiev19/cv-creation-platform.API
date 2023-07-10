using Data.Models.CV;
using Data.Models.Auth;

namespace CVCreationPlatform.ResumeService.Models
{
    public class ResumeDTO
    {
        public int UserId { get; set; }

        public string? Title { get; set; }

        public virtual ICollection<CertificateDTO> Certificates { get; set; } = new List<CertificateDTO>();

        public virtual ICollection<EducationDTO> Educations { get; set; } = new List<EducationDTO>();

        public virtual ICollection<PersonalInfoDTO> PersonalInfos { get; set; } = new List<PersonalInfoDTO>();


        public virtual ICollection<WorkExperienceDTO> WorkExperiences { get; set; } = new List<WorkExperienceDTO>();

        public virtual ICollection<LocationDTO> Locations { get; set; } = new List<LocationDTO>();

        public virtual ICollection<string?> Skills { get; set; } = new List<string?>();
    }
}