using Data.Models.CV;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.ResumeService.Models
{
	public class EducationDTO
	{
		[StringLength(40)]
		[Unicode(false)]
		public string? InstituteName { get; set; }

		[StringLength(30)]
		[Unicode(false)]
		public string? Degree { get; set; }

		[StringLength(30)]
		[Unicode(false)]
		public string? FieldOfStudy { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }
	}
}
