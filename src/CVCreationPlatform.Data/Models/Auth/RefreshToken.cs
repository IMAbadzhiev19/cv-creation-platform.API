using Data.Models.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVCreationPlatform.Data.Models.Auth;

public class RefreshToken
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string? Token { get; set; } = null!;
    public DateTime? TokenCreated { get; set; }
    public DateTime? TokenExpires { get; set; }

    [InverseProperty("RefreshToken")]
    public User? User { get; set; }
}
