using Data.Models.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.CV;

public class Cv
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string OwnerImage { get; set; }
    public ICollection<Section> Sections { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User User { get; set; }
}