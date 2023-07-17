using CVCreationPlatform.Data.Models.CV;
using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models;
using Data.Data;
using Data.Models.CV;
using Microsoft.EntityFrameworkCore;

namespace CVCreationPlatform.ResumeService.Implementations;

public class CvService : ICvService
{
    private readonly ApplicationDbContext _context;
    private readonly IFileService _fileService;

    public CvService(ApplicationDbContext context, IFileService fileService)
        => (_context, _fileService) = (context, fileService);

    public async Task<Guid> CreateResumeAsync(ResumeDTO resumeModel, Guid id = default)
    {
        var personalInfo = resumeModel.PersonalInfo;
        if (personalInfo != null)
        {
            if (personalInfo.Photo != null)
            {
                var photoUrl = await this._fileService.UploadImage(personalInfo.Photo);
                personalInfo.PhotoUrl = photoUrl;
            }
        }

        var resume = await this.MapToResumeAsync(resumeModel);

        if (id != default)
            resume.Id = id;

        await this._context.Resumes.AddAsync(resume);
        await this._context.Certificates.AddRangeAsync(resume.Certificates);
        await this._context.Educations.AddRangeAsync(resume.Educations);
        await this._context.Languages.AddRangeAsync(resume.Languages);
        await this._context.WorkExperiences.AddRangeAsync(resume.WorkExperiences);
        await this._context.Skills.AddRangeAsync(resume.Skills);

        await this._context.SaveChangesAsync();
        return resume.Id;
    }

    public async Task<bool> UpdateResumeAsync(Guid oldResumeId, ResumeDTO newResumeModel)
    {
        await this.DeleteResumeAsync(oldResumeId);
        await this.CreateResumeAsync(newResumeModel, oldResumeId);

        await this._context.SaveChangesAsync();
        return true;
    }

    public async Task DeleteResumeAsync(Guid resumeId)
    {
        var resumeToDelete = await this._context.Resumes.FirstOrDefaultAsync(x => x.Id == resumeId);

        if (resumeToDelete == null)
            throw new ArgumentException("Invalid id");

        var personalInfoToRemove = await this._context.PersonalInfos.FirstOrDefaultAsync(x => x.ResumeId == resumeId);
        if (personalInfoToRemove != null)
            this._context.PersonalInfos.Remove(personalInfoToRemove);

        var unknownSectionToRemove = await this._context.UnknownSections.FirstOrDefaultAsync(x => x.ResumeId == resumeId);
        if (unknownSectionToRemove != null)
            this._context.UnknownSections.Remove(unknownSectionToRemove);

        var workExperiencesToRemove = await this._context.WorkExperiences.Where(x => x.ResumeId == resumeId).ToListAsync();
        if (workExperiencesToRemove.Count != 0)
            this._context.WorkExperiences.RemoveRange(workExperiencesToRemove);

        var certificatesToRemove = await this._context.Certificates.Where(x => x.ResumeId == resumeId).ToListAsync();
        if (certificatesToRemove.Count != 0)
            this._context.Certificates.RemoveRange(certificatesToRemove);

        var languagesToRemove = await this._context.Languages.Where(x => x.ResumeId == resumeId).ToListAsync();
        if (languagesToRemove.Count != 0)
            this._context.Languages.RemoveRange(languagesToRemove);

        var educationsToRemove = await this._context.Educations.Where(x => x.ResumeId == resumeId).ToListAsync();
        if (educationsToRemove.Count != 0)
            this._context.Educations.RemoveRange(educationsToRemove);

        var skillsToRemove = await this._context.Skills.Where(x => x.Resumes.All(x => x.Id == resumeId)).ToListAsync();
        if (skillsToRemove.Count != 0)
            this._context.Skills.RemoveRange(skillsToRemove);

        var template = resumeToDelete.Template;
        if (template != null)
            template.Resumes.Remove(resumeToDelete);

        this._context.Resumes.Remove(resumeToDelete);
        await this._context.SaveChangesAsync();
    }

    public async Task<ResumeDTO> GetResumeByIdAsync(Guid resumeId)
    {
        var resumeToBeReturned = await this._context.Resumes
            .Include(r => r.PersonalInfo)
            .Include(r => r.UnknownSection)
            .Include(r => r.Template)
            .Include(r => r.WorkExperiences)
            .Include(r => r.Certificates)
            .Include(r => r.Languages)
            .Include (r => r.Skills)
            .Include(r => r.Educations)
            .FirstOrDefaultAsync(x => x.Id == resumeId);

        if (resumeToBeReturned == null)
            throw new ArgumentException("Invalid id");

        return new ResumeDTO(resumeToBeReturned);
    }

    public async Task<List<ResumeDTO>> GetResumesByUserIdAsync(Guid userId)
    {
        var user = await this._context.Users.FindAsync(userId);
        if (user == null)
            throw new ArgumentException("Invalid id");

        var resumes = await this._context.Resumes
            .Include(r => r.PersonalInfo)
            .Include(r => r.UnknownSection)
            .Include(r => r.Template)
            .Include(r => r.WorkExperiences)
            .Include(r => r.Certificates)
            .Include(r => r.Languages)
            .Include(r => r.Skills)
            .Include(r => r.Educations)
            .Where(x => x.UserId == userId)
            .Select(x => new ResumeDTO(x))
            .ToListAsync();

        return resumes;
    }


    private async Task<Resume> MapToResumeAsync(ResumeDTO resumeModel)
    {
        return await Task.Run(() =>
        {
            var initialResume = new Resume
            {
                Id = Guid.NewGuid(),
                UserId = resumeModel.UserId,
                Title = resumeModel.Title,
                CreationDate = resumeModel.CreationDate,
            };

            if(resumeModel.PersonalInfo != null)
            {
                initialResume.PersonalInfo = new PersonalInfo
                {
                    PhotoUrl = resumeModel.PersonalInfo.PhotoUrl,
                    ResumeId = initialResume.Id,
                    Resume = initialResume,
                    Description = resumeModel.PersonalInfo.Description,
                    FirstName = resumeModel.PersonalInfo.FirstName,
                    MiddleName = resumeModel.PersonalInfo.MiddleName,
                    LastName = resumeModel.PersonalInfo.LastName,
                    Address = resumeModel.PersonalInfo.Address,
                    PhoneNumber = resumeModel.PersonalInfo.PhoneNumber,
                    Email = resumeModel.PersonalInfo.Email,
                };
            }

            if (resumeModel.UnknownSection != null)
            {
                initialResume.UnknownSection = new UnknownSection
                {
                    Title = resumeModel.UnknownSection.Title,
                    Description = resumeModel.UnknownSection.Description,
                    StartDate = resumeModel.UnknownSection.StartDate,
                    EndDate = resumeModel.UnknownSection.EndDate,
                    ResumeId = initialResume.Id,
                    Resume = initialResume
                };
            }

            if (resumeModel.Template != null)
            {
                var template = this._context.Templates.FirstOrDefault(x => x.TemplateName == resumeModel.Template.TemplateName);
                if (template != null)
                {
                    initialResume.TemplateId = template.Id;
                }
                else
                    initialResume.Template = new Template
                    {
                        TemplateName = resumeModel.Template.TemplateName,
                    };
            }

            if (resumeModel.Certificates.Count != 0)
            {
                initialResume.Certificates = new List<Certificate>(resumeModel.Certificates
                .Select(dto => new Certificate
                {
                    ResumeId = initialResume.Id,
                    Resume = initialResume,
                    CertificateName = dto.CertificateName,
                    IssuingOrganization = dto.IssuingOrganization,
                    IssueDate = dto.IssueDate,
                })
            );
            }

            if (resumeModel.Educations.Count != 0)
            {
                initialResume.Educations = new List<Education>(resumeModel.Educations
                    .Select(dto => new Education
                    {
                        ResumeId = initialResume.Id,
                        Resume = initialResume,
                        InstituteName = dto.InstituteName,
                        Degree = dto.Degree,
                        FieldOfStudy = dto.FieldOfStudy,
                        StartDate = dto.StartDate,
                        EndDate = dto.EndDate
                    })
                );
            }

            if (resumeModel.Languages.Count != 0)
            {
                initialResume.Languages = new List<Language>(resumeModel.Languages
                    .Select(dto => new Language
                    {
                        Name = dto.Name,
                        Level = dto.Level,
                        ResumeId = initialResume.Id,
                        Resume = initialResume,
                    })
                );
            }

            if (resumeModel.WorkExperiences.Count != 0)
            {
                initialResume.WorkExperiences = new List<WorkExperience>(resumeModel.WorkExperiences
                    .Select(dto => new WorkExperience
                    {
                        ResumeId = initialResume.Id,
                        Resume = initialResume,
                        CompanyName = dto.CompanyName,
                        Position = dto.Position,
                        Location = dto.Location,
                        StartDate = dto.StartDate,
                        EndDate = dto.EndDate,
                        Description = dto.Description
                    })
                );
            }

            if (resumeModel.Skills.Count != 0)
            {
                initialResume.Skills = new List<Skill>(resumeModel.Skills
                    .Select(dto => new Skill
                    {
                        SkillName = dto.SkillName,
                    })
                );

                foreach (var skill in initialResume.Skills)
                    skill.Resumes.Add(initialResume);
            }

            return initialResume;
        });
    }
}