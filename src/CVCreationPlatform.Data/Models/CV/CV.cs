using Data.Models.Auth;

namespace Data.Models.CV;

public class Cv
{
    public string OwnerImage { get; set; }
    public ICollection<Section> Sections { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}