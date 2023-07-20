using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models.DTO;
using CVCreationPlatform.ResumeService.Models.ViewModels;
using Data.Data;
using Data.Models.CV;
using Microsoft.EntityFrameworkCore;

namespace CVCreationPlatform.ResumeService.Implementations;

public class CertificateService : ICertificateService
{
    private readonly ApplicationDbContext _context;

    public CertificateService(ApplicationDbContext context)
        => _context = context;

    public async Task<bool> AssignCertificateToResume(Guid resumeId, CertificateDTO certificateDTO)
    {
        var resume = await _context.Resumes.FirstOrDefaultAsync(r => r.Id == resumeId);
        if (resume == null)
            throw new ArgumentException("Invalid certificate id");
        DateTime? issueDate = null;
        if (certificateDTO.IssueDate != null)
        {
            bool isDateParsed = DateTime.TryParse(certificateDTO.IssueDate, out DateTime parsedDate);
            if (!isDateParsed)
                throw new ArgumentException("Invalid date format");
            issueDate = parsedDate;
        }
        var certificateToAdd = new Certificate
        {
            ResumeId = resume.Id,
            Resume = resume,
            CertificateName = certificateDTO.CertificateName,
            IssuingOrganization = certificateDTO.IssuingOrganization,
            IssueDate = issueDate
        };

        resume.Certificates.Add(certificateToAdd);

        await this._context.Certificates.AddAsync(certificateToAdd);
        await this._context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateCertificate(int certificateId, CertificateDTO newCertificateDTO)
    {
        var certificate = await _context.Certificates.FindAsync(certificateId);
        if (certificate == null)
            throw new ArgumentException("Invalid certificate id");
        DateTime? issueDate = null;
        if (newCertificateDTO.IssueDate != null)
        {
            bool isDateParsed = DateTime.TryParse(newCertificateDTO.IssueDate, out DateTime parsedDate);
            if (!isDateParsed)
                throw new ArgumentException("Invalid date format");
            issueDate = parsedDate;
        }
        certificate.IssueDate = issueDate;
        certificate.CertificateName = newCertificateDTO.CertificateName;
        certificate.IssuingOrganization = newCertificateDTO.IssuingOrganization;

        await this._context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCertificate(int certificateId)
    {
        var certificateToRemove = await _context.Certificates.FindAsync(certificateId);
        if (certificateToRemove == null)
            throw new ArgumentException("Invalid language id");

        _context.Certificates.Remove(certificateToRemove);

        await this._context.SaveChangesAsync();
        return true;
    }
}
