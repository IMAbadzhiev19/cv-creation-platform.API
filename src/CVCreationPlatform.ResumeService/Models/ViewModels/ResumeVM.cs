using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models.ViewModels;

public class ResumeVM
{
    public ResumeVM()
    {

    }

    public ResumeVM(Resume resume)
    {
        Id = resume.Id;
        UserId = resume.UserId;
        Title = resume.Title;
        CreationDate = resume.CreationDate;
        PersonalInfo = new PersonalInfoVM(resume.PersonalInfo);
        UnknownSection = new UnknownSectionVM(resume.UnknownSection);
        Template = new TemplateVM(resume.Template);
        Certificates = new List<CertificateVM>(resume.Certificates.Select(x => new CertificateVM(x)));
        Educations = new List<EducationVM>(resume.Educations.Select(x => new EducationVM(x)));
        WorkExperiences = new List<WorkExperienceVM>(resume.WorkExperiences.Select(x => new WorkExperienceVM(x)));
        Languages = new List<LanguageVM>(resume.Languages.Select(x => new LanguageVM(x)));
        Skills = new List<SkillVM>(resume.Skills.Select(x => new SkillVM(x)));
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