using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models.ViewModels;

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
            CompanyName = workExperience.CompanyName;
            Position = workExperience.Position;
            Location = workExperience.Location;
            StartDate = workExperience.StartDate;
            EndDate = workExperience.EndDate;
            Description = workExperience.Description;
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
