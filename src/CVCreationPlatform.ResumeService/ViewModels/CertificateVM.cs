using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models;

public class CertificateVM
{
    public CertificateVM()
    {
        
    }

    public CertificateVM(Certificate certificate)
    {
        this.Id = certificate.Id;
        this.CertificateName = certificate.CertificateName;
	    this.IssuingOrganization = certificate.IssuingOrganization;
	    this.IssueDate = certificate.IssueDate;
    }
    public int Id { get; set; }
    public string? CertificateName { get; set; }

	public string? IssuingOrganization { get; set; }

	public DateTime? IssueDate { get; set; }
}
