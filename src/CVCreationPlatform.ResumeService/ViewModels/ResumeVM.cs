using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models;

public class ResumeVM
{
    public ResumeVM()
    {
        
    }

    public ResumeVM(Resume resume)
    {
        this.Id = resume.Id;
        this.UserId = resume.UserId;
        this.Title = resume.Title;
        this.CreationDate = resume.CreationDate;
        this.PersonalInfo = new PersonalInfoVM(resume.PersonalInfo);
        this.UnknownSection = new UnknownSectionVM(resume.UnknownSection);
        this.Template = new TemplateVM(resume.Template);
        this.Certificates = new List<CertificateVM>(resume.Certificates.Select(x => new CertificateVM(x)));
        this.Educations = new List<EducationVM>(resume.Educations.Select(x => new EducationVM(x)));
        this.WorkExperiences = new List<WorkExperienceVM>(resume.WorkExperiences.Select(x => new WorkExperienceVM(x)));
        this.Languages = new List<LanguageVM>(resume.Languages.Select(x => new LanguageVM(x)));
        this.Skills = new List<SkillVM>(resume.Skills.Select(x => new SkillVM(x)));
    }

    public Guid? Id { get; set; }

    public Guid UserId { get; set; }

    public string? Title { get; set; }

    public DateTime CreationDate { get; set; }

    public PersonalInfoVM? PersonalInfo { get; set; }
    
    public UnknownSectionVM? UnknownSection { get; set; }

    public TemplateVM? Template { get; set; }

    public virtual ICollection<CertificateVM> Certificates { get; set; } = new List<CertificateVM>();

    public virtual ICollection<EducationVM> Educations { get; set; } = new List<EducationVM>();

    public virtual ICollection<WorkExperienceVM> WorkExperiences { get; set; } = new List<WorkExperienceVM>();

    public virtual ICollection<LanguageVM> Languages { get; set; } = new List<LanguageVM>();

    public virtual ICollection<SkillVM> Skills { get; set; } = new List<SkillVM>();
}