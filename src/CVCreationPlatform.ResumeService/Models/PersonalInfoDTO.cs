using Data.Models.CV;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.ResumeService.Models
{
	public class PersonalInfoDTO
	{

		[StringLength(2058)]
		string? PhotoUrl { get; set; }


		[StringLength(100)]
		[Unicode(false)]
		public string? FullName { get; set; }

		[StringLength(50)]
		[Unicode(false)]
		public string? Address { get; set; }

		[StringLength(13)]
		[Unicode(false)]
		public string? PhoneNumber { get; set; }

		[StringLength(50)]
		[Unicode(false)]
		public string? Email { get; set; }

	}
}
