namespace CVCreationPlatform.ResumeService.DTO
{
    public class ResumeDTO
    {
        public Guid UserId { get; set; }

        public string? Title { get; set; }

        public DateTime CreationDate { get; set; }

        public PersonalInfoDTO? PersonalInfo { get; set; }

        public UnknownSectionDTO? UnknownSection { get; set; }

        public TemplateDTO? Template { get; set; }

        public virtual ICollection<CertificateDTO> Certificates { get; set; } = new List<CertificateDTO>();

        public virtual ICollection<EducationDTO> Educations { get; set; } = new List<EducationDTO>();

        public virtual ICollection<WorkExperienceDTO> WorkExperiences { get; set; } = new List<WorkExperienceDTO>();

        public virtual ICollection<LanguageDTO> Languages { get; set; } = new List<LanguageDTO>();

        public virtual ICollection<SkillDTO> Skills { get; set; } = new List<SkillDTO>();
    }
}
