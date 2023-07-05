namespace Data.Models.CV;

public class UserData
{
    public int UserDataId { get; set; }
    public UserPersonalDetails PersonalDetails { get; set; }
    public string Description { get; set; }
    public ICollection<UserEmployment>? EmployementHistory { get; set; }
    public ICollection<UserEducation>? Education { get; set; }
    public ICollection<string>? Skills { get; set; }
    public ICollection<string>? Certifications { get; set; }
    public ICollection<string>? Achievements { get; set; }
}