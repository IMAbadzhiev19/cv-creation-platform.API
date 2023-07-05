using Data.Models.CV;
using Microsoft.AspNetCore.Identity;

namespace Data.Models.Auth
{
    public class User : IdentityUser
    {
        public UserData UserData { get; set; }
        public ICollection<Cv> Cvs { get; set; }
    }
}