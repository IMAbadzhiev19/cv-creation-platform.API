using Data.Models.CV;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.Auth
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        [ForeignKey(nameof(UserData))]
        public int UserDataId { get; set; }
        public UserData? UserData { get; set; }

        public ICollection<Cv>? Cvs { get; set; }
    }
}