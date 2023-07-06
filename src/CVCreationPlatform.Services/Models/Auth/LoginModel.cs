using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.AuthService.Models.Auth;

public class LoginModel
{
    [MaxLength(15)]
    [MinLength(5)]
    [Required(ErrorMessage = "First name is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
