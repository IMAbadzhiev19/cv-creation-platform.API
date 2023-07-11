using Data.Models.CV;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.ResumeService.Models;

public class WorkExperienceDTO
{
    public WorkExperienceDTO()
    {
        
    }
    public WorkExperienceDTO(WorkExperience workExperience)
	{
		this.CompanyName = workExperience.CompanyName;
		this.Position = workExperience.Position;
		this.StartDate = workExperience.StartDate;
		this.EndDate = workExperience.EndDate;
		this.Description = workExperience.Description;
	}

	[StringLength(50)]
	[Unicode(false)]
	public string? CompanyName { get; set; }

	[StringLength(30)]
	[Unicode(false)]
	public string? Position { get; set; }

	public DateTime? StartDate { get; set; }

	public DateTime? EndDate { get; set; }

	[StringLength(40)]
	[Unicode(false)]
	public string? Description { get; set; }

}
