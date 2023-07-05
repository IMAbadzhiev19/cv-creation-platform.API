using Data.Models.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.CV;

public class UserData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public UserPersonalDetails? PersonalDetails { get; set; }
    public string? Description { get; set; }
    public ICollection<UserEmployment>? EmployementHistory { get; set; }
    public ICollection<UserEducation>? Education { get; set; }

    public List<string>? Skills { get; set; }
    public List<string>? Certifications { get; set; }
    public List<string>? Achievements { get; set; }


    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User User { get; set; }
}