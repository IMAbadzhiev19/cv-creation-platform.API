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

        var educationToAdd = new Education
        {
            ResumeId = resume.Id,
            Resume = resume,
            InstituteName = educationDTO.InstituteName,
            Degree = educationDTO.Degree,
            FieldOfStudy = educationDTO.FieldOfStudy,
            StartDate = educationDTO.StartDate,
            EndDate = educationDTO.EndDate
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

        education.InstituteName = newEducationDTO.InstituteName;
        education.Degree = newEducationDTO.Degree;
        education.FieldOfStudy = newEducationDTO.FieldOfStudy;
        education.StartDate = newEducationDTO.StartDate;
        education.EndDate = newEducationDTO.EndDate;

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
