using Data.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Data.Models.CV;
using CVCreationPlatform.Data.Models.Auth;

namespace Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<ResumeTemplate> ResumeTemplates { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}