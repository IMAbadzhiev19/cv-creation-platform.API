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

	public string? CertificateName { get; set; }

	public string? IssuingOrganization { get; set; }

	public DateTime? IssueDate { get; set; }
}
