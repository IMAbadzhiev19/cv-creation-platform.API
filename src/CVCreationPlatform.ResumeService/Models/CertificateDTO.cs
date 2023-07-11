using Data.Models.CV;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.ResumeService.Models;

public class CertificateDTO
{
    public CertificateDTO()
    {
        
    }

    public CertificateDTO(Certificate certificate)
    {
        this.CertificateName = certificate.CertificateName;
	this.IssuingOrganization = certificate.IssuingOrganization;
	this.IssueDate = certificate.IssueDate;
    }

    [StringLength(50)]
	[Unicode(false)]
	public string? CertificateName { get; set; }

	[StringLength(60)]
	[Unicode(false)]
	public string? IssuingOrganization { get; set; }

	public DateTime? IssueDate { get; set; }
}
