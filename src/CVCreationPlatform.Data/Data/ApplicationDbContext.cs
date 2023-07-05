using Data.Models.Auth;
using Data.Models.CV;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Cv> Cvs { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<UserData> UsersData { get; set; }
        public DbSet<UserEducation> UsersEducation { get; set; }
        public DbSet<UserEmployment> UsersEmployments { get; set; }
        public DbSet<UserPersonalDetails> UsersPersonalDetails { get; set; }
    }
}