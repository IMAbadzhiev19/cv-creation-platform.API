using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.CV;

public class UserEducation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string InstituteName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string City { get; set; }

    [ForeignKey(nameof(UserData))]
    public int UserDatId { get; set; }
    public UserData UserData { get; set; }
}
