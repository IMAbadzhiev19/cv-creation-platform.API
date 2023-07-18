using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models;

public class WorkExperienceVM
{
    public WorkExperienceVM()
    {
        
    }
    public WorkExperienceVM(WorkExperience? workExperience)
	{
		if (workExperience != null)
		{
			Id = workExperience.Id;
			this.CompanyName = workExperience.CompanyName;
			this.Position = workExperience.Position;
			this.Location = workExperience.Location;
			this.StartDate = workExperience.StartDate;
			this.EndDate = workExperience.EndDate;
			this.Description = workExperience.Description;
		}
	}
    public int Id { get; set; }
    public string? CompanyName { get; set; }

	public string? Position { get; set; }

	public DateTime? StartDate { get; set; }

	public DateTime? EndDate { get; set; }

	public string? Location { get; set; }

	public string? Description { get; set; }

}
