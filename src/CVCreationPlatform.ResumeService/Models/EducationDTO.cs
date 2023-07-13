using Data.Models.CV;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.ResumeService.Models;

public class EducationDTO
{
    public EducationDTO()
    {
        
    }
    public EducationDTO(Education education)
    {
        this.InstituteName = education.InstituteName;
		this.Degree = education.Degree;
		this.FieldOfStudy = education.FieldOfStudy;
		this.StartDate = education.StartDate;
		this.EndDate = education.EndDate;
    }

	[Unicode(false)]
	public string? InstituteName { get; set; }

	[Unicode(false)]
	public string? Degree { get; set; }

	[Unicode(false)]
	public string? FieldOfStudy { get; set; }

	public DateTime? StartDate { get; set; }

	public DateTime? EndDate { get; set; }
}
