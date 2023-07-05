using Data.Models.CV;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
        }

        public DbSet<Cv> Cvs { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<UserData> UsersData { get; set; }
        public DbSet<UserEducation> UsersEducation { get; set; }
        public DbSet<UserEmployment> UsersEmployments { get; set; }
        public DbSet<UserPersonalDetails> UsersPersonalDetails { get; set; }
    }
}