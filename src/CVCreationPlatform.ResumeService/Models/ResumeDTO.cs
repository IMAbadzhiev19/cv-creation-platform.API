using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models;

public class ResumeDTO
{
    public ResumeDTO()
    {
        
    }

    public ResumeDTO(Resume resume)
    {
        this.UserId = resume.UserId;
        this.Title = resume.Title;
        this.CreationDate = resume.CreationDate;
        this.PersonalInfo = new PersonalInfoDTO(resume.PersonalInfo);
        this.UnknownSection = new UnknownSectionDTO(resume.UnknownSection);
        this.Template = new TemplateDTO(resume.Template);
        this.Certificates = new List<CertificateDTO>(resume.Certificates.Select(x => new CertificateDTO(x)));
        this.Educations = new List<EducationDTO>(resume.Educations.Select(x => new EducationDTO(x)));
        this.WorkExperiences = new List<WorkExperienceDTO>(resume.WorkExperiences.Select(x => new WorkExperienceDTO(x)));
        this.Languages = new List<LanguageDTO>(resume.Languages.Select(x => new LanguageDTO(x)));
        this.Skills = new List<SkillsDTO>(resume.Skills.Select(x => new SkillsDTO(x.SkillName)));
    }

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

    public virtual ICollection<SkillsDTO> Skills { get; set; } = new List<SkillsDTO>();
}