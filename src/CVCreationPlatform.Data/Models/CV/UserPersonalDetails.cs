using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.CV;

public class UserPersonalDetails
{
    public int UserPersonalDetailsId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Country { get; set; }
    public string City { get; set; }

    [ForeignKey(nameof(UserData))]
    public int UserDataId { get; set; }
    public UserData UserData { get; set; }
}
