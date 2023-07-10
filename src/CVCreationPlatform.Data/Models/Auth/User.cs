using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CVCreationPlatform.Data.Models.Auth;
using Data.Models.CV;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.Auth;

public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Username { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Password { get; set; }

	[InverseProperty("User")]
    public virtual ICollection<Resume> Resumes { get; set; } = new List<Resume>();

    [ForeignKey(nameof(RefreshTokenId))]
    [InverseProperty("User")]
    public int? RefreshTokenId { get; set; }
    public RefreshToken? RefreshToken { get; set; }
}
