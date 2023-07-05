using Data.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Models.CV;

public class Section
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Heading { get; set; }
    public string? Subheading { get; set; }
    public string Data { get; set; }
    public bool HasSubSections { get; set; }
    public ICollection<Section>? SubSections { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}