using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models.ViewModels;

public class PersonalInfoVM
{
    public PersonalInfoVM()
    {

    }
    public PersonalInfoVM(PersonalInfo? personalInfo)
    {
        if (personalInfo != null)
        {
            Id = personalInfo.Id;
            PhotoUrl = personalInfo.PhotoUrl;
            FirstName = personalInfo.FirstName;
            MiddleName = personalInfo.MiddleName;
            LastName = personalInfo.LastName;
            Description = personalInfo.Description;
            Address = personalInfo.Address;
            PhoneNumber = personalInfo.PhoneNumber;
            Email = personalInfo.Email;
        }
    }
    public int Id { get; set; }
    public string? PhotoUrl { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }
}