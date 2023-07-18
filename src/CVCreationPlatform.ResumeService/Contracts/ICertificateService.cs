using CVCreationPlatform.ResumeService.Models.DTO;

namespace CVCreationPlatform.ResumeService.Contracts;

public interface ICertificateService
{
    Task<bool> AssignCertificateToResume(Guid resumeId, CertificateDTO certificateDTO);
    Task<bool> UpdateCertificate(int certificateId, CertificateDTO newCertificateDTO);
    Task<bool> DeleteCertificate(int certificateId);
}
