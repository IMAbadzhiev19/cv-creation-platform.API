using Data.Models.CV;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.ResumeService.Models
{
	public class CertificateDTO
	{
		[StringLength(50)]
		[Unicode(false)]
		public string? CertificateName { get; set; }

		[StringLength(60)]
		[Unicode(false)]
		public string? IssuingOrganization { get; set; }

		public DateTime? IssueDate { get; set; }
	}
}
