using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.AuthService.Models.Auth;

public class RegistrationModel
{
    [MaxLength(15)]
    [MinLength(5)]
    [Required(ErrorMessage = "First name is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
