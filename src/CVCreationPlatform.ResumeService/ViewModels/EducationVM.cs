using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models;

public class EducationVM
{
    public EducationVM()
    {
        
    }
    public EducationVM(Education education)
    {
        this.Id = education.Id;
        this.InstituteName = education.InstituteName;
		this.Degree = education.Degree;
		this.FieldOfStudy = education.FieldOfStudy;
		this.StartDate = education.StartDate;
		this.EndDate = education.EndDate;
    }

    public int Id { get; set; }
    public string? InstituteName { get; set; }

	public string? Degree { get; set; }

	public string? FieldOfStudy { get; set; }

	public DateTime? StartDate { get; set; }

	public DateTime? EndDate { get; set; }
}
