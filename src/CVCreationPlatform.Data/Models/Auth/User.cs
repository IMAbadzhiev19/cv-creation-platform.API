using Data.Models.CV;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.Auth
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }


        [ForeignKey(nameof(UserData))]
        public int? UserDataId { get; set; }
        public UserData? UserData { get; set; }

        public ICollection<Cv>? Cvs { get; set; }
    }
}