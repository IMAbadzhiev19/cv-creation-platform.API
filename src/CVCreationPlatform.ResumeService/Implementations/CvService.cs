using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models;
using Data.Data;
using Data.Models.CV;
using Microsoft.EntityFrameworkCore;

namespace CVCreationPlatform.ResumeService.Implementations
{
    public class CvService : ICvService
    {
        private readonly ApplicationDbContext _context;

        public CvService(ApplicationDbContext context)
            => _context = context;

        public async Task<Guid> CreateResumeAsync(ResumeDTO resumeModel)
        {
            var resume = await this.MapResume(resumeModel);

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
            throw new NotImplementedException();
        }

        public async Task DeleteResumeAsync(Guid resumeId)
        {
            var resumeToDelete = await this._context.Resumes.FirstOrDefaultAsync(x => x.Id == resumeId);

            if (resumeToDelete == null)
                throw new ArgumentException("Invalid id");

            this._context.Resumes.Remove(resumeToDelete);
            await this._context.SaveChangesAsync();
        }

        public async Task<(ResumeDTO, DateTime, DateTime)> GetResumeByIdAsync(Guid resumeId)
        {
            var resumeToBeReturned = await this._context.Resumes
                .Include(r => r.PersonalInfo)
                .Include(r => r.WorkExperiences)
                .Include(r => r.Certificates)
                .Include(r => r.Languages)
                .Include (r => r.Skills)
                .Include(r => r.Educations)
                .FirstOrDefaultAsync(x => x.Id == resumeId);

            if (resumeToBeReturned == null)
                throw new ArgumentException("Invalid id");

            var resumeDto = new ResumeDTO(resumeToBeReturned);
            return (resumeDto, resumeToBeReturned.CreationDate, resumeToBeReturned.LastModifiedDate);
        }

        private async Task<Resume> MapResume(ResumeDTO resumeModel)
        {
            return await Task.Run(() =>
            {
                var initialResume = new Resume
                {
                    Id = Guid.NewGuid(),
                    UserId = resumeModel.UserId,
                    Title = resumeModel.Title,
                };

                if(resumeModel.PersonalInfo != null)
                {
                    initialResume.PersonalInfo = new PersonalInfo
                    {
                        PhotoUrl = resumeModel.PersonalInfo.PhotoUrl,
                        ResumeId = initialResume.Id,
                        Resume = initialResume,
                        FullName = resumeModel.PersonalInfo.FullName,
                        Address = resumeModel.PersonalInfo.Address,
                        PhoneNumber = resumeModel.PersonalInfo.PhoneNumber,
                        Email = resumeModel.PersonalInfo.Email,
                    };
                }

                if(resumeModel.Certificates.Count != 0)
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
                            SkillName = dto,
                        })
                    );

                    foreach (var skill in initialResume.Skills)
                        skill.Resumes.Add(initialResume);
                }

                return initialResume;
            });
        }
    }
}