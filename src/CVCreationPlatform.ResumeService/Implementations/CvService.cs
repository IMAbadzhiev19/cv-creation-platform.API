using Azure.Communication.Email;
using CVCreationPlatform.Data.Models.CV;
using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models.DTO;
using CVCreationPlatform.ResumeService.Models.ViewModels;
using Data.Data;
using Data.Models.CV;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace CVCreationPlatform.ResumeService.Implementations;

public class CvService : ICvService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public CvService(IConfiguration configuration, ApplicationDbContext context)
        => (_configuration, _context) = (configuration, context);

    public async Task<Guid> CreateResumeAsync(ResumeDTO resumeModel, string photoUrl)
    {
        var resume = await MapToResumeAsync(resumeModel, photoUrl);

        await _context.Resumes.AddAsync(resume);
        await _context.SaveChangesAsync();
        return resume.Id;
    }

    public async Task<bool> UpdateResumeAsync(Guid oldResumeId, ResumeDTO newResumeModel, string photoUrl)
    {
        var resume = await this._context.Resumes
            .Include(r => r.PersonalInfo)
            .Include(r => r.UnknownSection)
            .Include(r => r.Template)
            .FirstOrDefaultAsync(r => r.Id == oldResumeId);
        if (resume == null)
            throw new ArgumentException("Invalid resume id");

        resume.Title = newResumeModel.Title;
        resume.CreationDate = newResumeModel.CreationDate;

        if (string.IsNullOrEmpty(photoUrl))
            if (resume.PersonalInfo != null)
                if (resume.PersonalInfo.PhotoUrl != null)
                    photoUrl = resume.PersonalInfo.PhotoUrl;

        if (newResumeModel.PersonalInfo != null)
        {
            if (resume.PersonalInfo == null)
                resume.PersonalInfo = new PersonalInfo();
            resume.PersonalInfo.Description = newResumeModel.PersonalInfo.Description;
            resume.PersonalInfo.FirstName = newResumeModel.PersonalInfo.FirstName;
            resume.PersonalInfo.MiddleName = newResumeModel.PersonalInfo.MiddleName;
            resume.PersonalInfo.LastName = newResumeModel.PersonalInfo.LastName;
            resume.PersonalInfo.Address = newResumeModel.PersonalInfo.Address;
            resume.PersonalInfo.PhoneNumber = newResumeModel.PersonalInfo.PhoneNumber;
            resume.PersonalInfo.Email = newResumeModel.PersonalInfo.Email;
            resume.PersonalInfo.PhotoUrl = photoUrl;
        }

        if (newResumeModel.UnknownSection != null)
        {
            if (resume.UnknownSection == null)
                resume.UnknownSection = new UnknownSection();
            resume.UnknownSection.Title = newResumeModel.UnknownSection.Title;
            resume.UnknownSection.Description = newResumeModel.UnknownSection.Description;
            resume.UnknownSection.StartDate = newResumeModel.UnknownSection.StartDate;
            resume.UnknownSection.EndDate = newResumeModel.UnknownSection.EndDate;
        }

        if (newResumeModel.Template != null)
        {
            var template = _context.Templates.FirstOrDefault(x => x.TemplateName == newResumeModel.Template.TemplateName);
            if (template != null)
            {
                resume.TemplateId = template.Id;
            }
            else
            {
                Template newTempplate = new Template
                {
                    TemplateName = newResumeModel.Template.TemplateName,
                };
                await _context.Templates.AddAsync(newTempplate);
                resume.Template = newTempplate;
            }
        }

        await _context.SaveChangesAsync();
        return true;

    }

    public async Task DeleteResumeAsync(Guid resumeId)
    {
        var resumeToDelete = await _context.Resumes.FirstOrDefaultAsync(x => x.Id == resumeId);

        if (resumeToDelete == null)
            throw new ArgumentException("Invalid id");

        var personalInfoToRemove = await _context.PersonalInfos.FirstOrDefaultAsync(x => x.ResumeId == resumeId);
        if (personalInfoToRemove != null)
            _context.PersonalInfos.Remove(personalInfoToRemove);

        var unknownSectionToRemove = await _context.UnknownSections.FirstOrDefaultAsync(x => x.ResumeId == resumeId);
        if (unknownSectionToRemove != null)
            _context.UnknownSections.Remove(unknownSectionToRemove);

        var workExperiencesToRemove = await _context.WorkExperiences.Where(x => x.ResumeId == resumeId).ToListAsync();
        if (workExperiencesToRemove.Count != 0)
            _context.WorkExperiences.RemoveRange(workExperiencesToRemove);

        var certificatesToRemove = await _context.Certificates.Where(x => x.ResumeId == resumeId).ToListAsync();
        if (certificatesToRemove.Count != 0)
            _context.Certificates.RemoveRange(certificatesToRemove);

        var languagesToRemove = await _context.Languages.Where(x => x.ResumeId == resumeId).ToListAsync();
        if (languagesToRemove.Count != 0)
            _context.Languages.RemoveRange(languagesToRemove);

        var educationsToRemove = await _context.Educations.Where(x => x.ResumeId == resumeId).ToListAsync();
        if (educationsToRemove.Count != 0)
            _context.Educations.RemoveRange(educationsToRemove);

        var skillsToRemove = await _context.Skills.Where(x => x.Resumes.All(x => x.Id == resumeId)).ToListAsync();
        if (skillsToRemove.Count != 0)
            _context.Skills.RemoveRange(skillsToRemove);

        var template = resumeToDelete.Template;
        if (template != null)
            template.Resumes.Remove(resumeToDelete);

        _context.Resumes.Remove(resumeToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<ResumeVM> GetResumeByIdAsync(Guid resumeId)
    {
        var resumeToBeReturned = await _context.Resumes
            .Include(r => r.PersonalInfo)
            .Include(r => r.UnknownSection)
            .Include(r => r.Template)
            .Include(r => r.WorkExperiences)
            .Include(r => r.Certificates)
            .Include(r => r.Languages)
            .Include(r => r.Skills)
            .Include(r => r.Educations)
            .FirstOrDefaultAsync(x => x.Id == resumeId);

        if (resumeToBeReturned == null)
            throw new ArgumentException("Invalid id");

        return new ResumeVM(resumeToBeReturned);
    }

    public async Task<List<ResumeVM>> GetResumesByUserIdAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new ArgumentException("Invalid id");

        var resumes = await _context.Resumes
            .Include(r => r.PersonalInfo)
            .Include(r => r.UnknownSection)
            .Include(r => r.Template)
            .Include(r => r.WorkExperiences)
            .Include(r => r.Certificates)
            .Include(r => r.Languages)
            .Include(r => r.Skills)
            .Include(r => r.Educations)
            .Where(x => x.UserId == userId)
            .Select(x => new ResumeVM(x))
            .ToListAsync();

        return resumes;
    }

    private async Task<Resume> MapToResumeAsync(ResumeDTO resumeModel, string photoUrl)
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

            if (resumeModel.PersonalInfo != null)
            {
                initialResume.PersonalInfo = new PersonalInfo
                {
                    ResumeId = initialResume.Id,
                    Resume = initialResume,
                    Description = resumeModel.PersonalInfo.Description,
                    FirstName = resumeModel.PersonalInfo.FirstName,
                    MiddleName = resumeModel.PersonalInfo.MiddleName,
                    LastName = resumeModel.PersonalInfo.LastName,
                    Address = resumeModel.PersonalInfo.Address,
                    PhoneNumber = resumeModel.PersonalInfo.PhoneNumber,
                    Email = resumeModel.PersonalInfo.Email,
                    PhotoUrl = photoUrl
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
                var template = _context.Templates.FirstOrDefault(x => x.TemplateName == resumeModel.Template.TemplateName);
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

            return initialResume;
        });
    }

    public async Task ShareResumeAsync(ShareDTO shareDto)
    {
        EmailClient emailClient = new EmailClient(this._configuration["Azure:EmailClient:ConnectionString"]);
        EmailContent emailContent = new EmailContent($"{shareDto.FirstName} {shareDto.LastName} shared this resume with you!");
        emailContent.PlainText = "Send with Microsoft Azure";
        var emailAddresses = new List<EmailAddress> { new EmailAddress(shareDto.ReceiptantEmail) };
        var emailReceiptents = new EmailRecipients(emailAddresses);
        EmailMessage emailMessage = new EmailMessage("DoNotReply@1700a80a-2d42-4be7-8886-41da41cbc9cc.azurecomm.net", emailReceiptents, emailContent);

        using (var stream = shareDto.File.OpenReadStream())
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int)shareDto.File.Length);
            BinaryData binaryData = new BinaryData(bytes);

            var attachment = new EmailAttachment(shareDto.File.FileName, "application/pdf", binaryData);
            emailMessage.Attachments.Add(attachment);
        }

        await emailClient.SendAsync(Azure.WaitUntil.Completed, emailMessage);
    }
}