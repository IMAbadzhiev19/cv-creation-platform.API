using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models.DTO;
using Data.Data;
using Data.Models.CV;
using Microsoft.EntityFrameworkCore;

namespace CVCreationPlatform.ResumeService.Implementations;

public class EducationService : IEducationService
{
    private readonly ApplicationDbContext _context;

    public EducationService(ApplicationDbContext context)
        => _context = context;

    public async Task<bool> AssignEducationToResume(Guid resumeId, EducationDTO educationDTO)
    {
        var resume = await _context.Resumes.FirstOrDefaultAsync(r => r.Id == resumeId);
        if (resume == null)
            throw new ArgumentException("Invalid education id");
        DateTime? startDate = null;
        DateTime? endDate = null;
        if (educationDTO.StartDate != null)
        {
            bool isStartDateParsed = DateTime.TryParse(educationDTO.StartDate, out DateTime parsedStartDate);
            if (!isStartDateParsed)
                throw new ArgumentException("Invalid date format");
            startDate = parsedStartDate;
        }
        if (educationDTO.EndDate != null)
        {
            bool isEndDateParsed = DateTime.TryParse(educationDTO.EndDate, out DateTime parsedEndDate);
            if (!isEndDateParsed)
                throw new ArgumentException("Invalid date format");
            endDate = parsedEndDate;
        }
        var educationToAdd = new Education
        {
            ResumeId = resume.Id,
            Resume = resume,
            InstituteName = educationDTO.InstituteName,
            Degree = educationDTO.Degree,
            FieldOfStudy = educationDTO.FieldOfStudy,
            StartDate = startDate,
            EndDate = endDate
        };

        resume.Educations.Add(educationToAdd);

        await this._context.Educations.AddAsync(educationToAdd);
        await this._context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateEducation(int educationId, EducationDTO newEducationDTO)
    {
        var education = await _context.Educations.FindAsync(educationId);
        if (education == null)
            throw new ArgumentException("Invalid education id");
        DateTime? startDate = null;
        DateTime? endDate = null;
        if (newEducationDTO.StartDate != null)
        {
            bool isStartDateParsed = DateTime.TryParse(newEducationDTO.StartDate, out DateTime parsedStartDate);
            if (!isStartDateParsed)
                throw new ArgumentException("Invalid date format");
            startDate = parsedStartDate;
        }
        if (newEducationDTO.EndDate != null)
        {
            bool isEndDateParsed = DateTime.TryParse(newEducationDTO.EndDate, out DateTime parsedEndDate);
            if (!isEndDateParsed)
                throw new ArgumentException("Invalid date format");
            endDate = parsedEndDate;
        }
        education.InstituteName = newEducationDTO.InstituteName;
        education.Degree = newEducationDTO.Degree;
        education.FieldOfStudy = newEducationDTO.FieldOfStudy;
        education.StartDate = startDate;
        education.EndDate = endDate;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteEducation(int educationId)
    {
        var educationToRemove = await _context.Educations.FindAsync(educationId);
        if (educationToRemove == null)
            throw new ArgumentException("Invalid education id");

        _context.Educations.Remove(educationToRemove);
        await this._context.SaveChangesAsync();
        return true;
    }
}
