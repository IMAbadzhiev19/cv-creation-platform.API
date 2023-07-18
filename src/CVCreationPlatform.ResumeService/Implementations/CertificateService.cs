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

        var certificateToAdd = new Certificate
        {
            ResumeId = resume.Id,
            Resume = resume,
            CertificateName = certificateDTO.CertificateName,
            IssuingOrganization = certificateDTO.IssuingOrganization,
            IssueDate = certificateDTO.IssueDate
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

        certificate.IssueDate = newCertificateDTO.IssueDate;
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
