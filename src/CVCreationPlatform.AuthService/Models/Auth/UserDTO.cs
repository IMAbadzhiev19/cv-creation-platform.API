using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.AuthService.Models.Auth
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string? Username { get; set; }

        public string? Jwt { get; set; }

        public DateTime? JWTExpirationDate { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpirationDate { get; set; }
    }
}