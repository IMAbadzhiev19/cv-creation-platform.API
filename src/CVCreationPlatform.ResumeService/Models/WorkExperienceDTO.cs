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
		this.Location = workExperience.Location;
		this.StartDate = workExperience.StartDate;
		this.EndDate = workExperience.EndDate;
		this.Description = workExperience.Description;
	}

	public string? CompanyName { get; set; }

	public string? Position { get; set; }

	public DateTime? StartDate { get; set; }

	public DateTime? EndDate { get; set; }

	public string? Location { get; set; }

	public string? Description { get; set; }

}
