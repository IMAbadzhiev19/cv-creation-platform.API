using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.Services.Models.Auth
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression("^[A-Za-zА-Яа-я]{2,50}$")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression("^[A-Za-zА-Яа-я]{2,50}$")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        
        public string PhoneNumber { get; set; }
    }
}
