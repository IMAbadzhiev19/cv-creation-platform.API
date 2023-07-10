using Data.Models.CV;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.ResumeService.Models
{
	public class LocationDTO
	{
		[StringLength(30)]
		[Unicode(false)]
		public string? City { get; set; }

		[StringLength(30)]
		[Unicode(false)]
		public string? Country { get; set; }

		[StringLength(30)]
		[Unicode(false)]
		public string? State { get; set; }
	}
}
