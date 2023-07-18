using Microsoft.AspNetCore.Http;

namespace CVCreationPlatform.ResumeService.Models.DTO
{
    public class PersonalInfoDTO
    {
        public IFormFile? Photo { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
    }
}
