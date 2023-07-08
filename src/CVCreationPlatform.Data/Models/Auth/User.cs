using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
	[StringLength(200)]
	public string? Token { get; set; } = null!;
	public DateTime? TokenCreated { get; set; }
	public DateTime? TokenExpires { get; set; }

	[InverseProperty("User")]
    public virtual ICollection<Resume> Resumes { get; set; } = new List<Resume>();
}
