using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.CV;

public class UserEmployment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string City { get; set; }
    public string Description { get; set; }

    [ForeignKey(nameof(UserData))]
    public int UserDataId { get; set; }
    public UserData UserData { get; set; }
}
