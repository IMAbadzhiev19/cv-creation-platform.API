using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models.ViewModels;

public class CertificateVM
{
    public CertificateVM()
    {

    }

    public CertificateVM(Certificate certificate)
    {
        Id = certificate.Id;
        CertificateName = certificate.CertificateName;
        IssuingOrganization = certificate.IssuingOrganization;
        IssueDate = certificate.IssueDate;
    }
    public int Id { get; set; }
    public string? CertificateName { get; set; }

    public string? IssuingOrganization { get; set; }

    public DateTime? IssueDate { get; set; }
}
