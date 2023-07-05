using Data.Models.Auth;
using Data.Models.CV;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Cv> Cvs { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<UserData> UsersData { get; set; }
        public DbSet<UserEducation> UsersEducation { get; set; }
        public DbSet<UserEmployment> UsersEmployments { get; set; }
        public DbSet<UserPersonalDetails> UsersPersonalDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>()
                .Property(ud => ud.Skills)
                .HasConversion(
                    skills => string.Join(", ", skills),
                    skillsString => skillsString.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            modelBuilder.Entity<UserData>()
                .Property(ud => ud.Certifications)
                .HasConversion(
                    certifications => string.Join(", ", certifications),
                    certificationsString => certificationsString.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            modelBuilder.Entity<UserData>()
                .Property(ud => ud.Achievements)
                .HasConversion(
                    achievements => string.Join(", ", achievements),
                    achievementsString => achievementsString.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserData)
                .WithOne()
                .HasForeignKey<UserData>(ud => ud.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Cvs)
                .WithOne(cv => cv.User)
                .HasForeignKey(cv => cv.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}