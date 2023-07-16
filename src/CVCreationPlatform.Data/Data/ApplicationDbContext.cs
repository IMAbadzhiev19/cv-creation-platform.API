using Data.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Data.Models.CV;
using CVCreationPlatform.Data.Models.Auth;
using CVCreationPlatform.Data.Models.CV;

namespace Data.Data;

/// <summary>
/// Application database context.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">Options.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }

    /// <summary>
    /// Gets or sets Users.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets Certificates.
    /// </summary>
    public DbSet<Certificate> Certificates { get; set; }

    /// <summary>
    /// Gets or sets Educations.
    /// </summary>
    public DbSet<Education> Educations { get; set; }

    /// <summary>
    /// Gets or sets PersonalInfos.
    /// </summary>
    public DbSet<PersonalInfo> PersonalInfos { get; set; }

    /// <summary>
    /// Gets or sets Resumes.
    /// </summary>
    public DbSet<Resume> Resumes { get; set; }

    /// <summary>
    /// Gets or sets Skills.
    /// </summary>
    public DbSet<Skill> Skills { get; set; }

    /// <summary>
    /// Gets or sets Templates.
    /// </summary>
    public DbSet<Template> Templates { get; set; }

    /// <summary>
    /// Gets or sets WorkExperiences.
    /// </summary>
    public DbSet<WorkExperience> WorkExperiences { get; set; }

    /// <summary>
    /// Gets or sets RefreshTokens.
    /// </summary>
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    /// <summary>
    /// Gets or sets Languages.
    /// </summary>
    public DbSet<Language> Languages { get; set; }

    /// <summary>
    /// Gets or sets UnknownSections.
    /// </summary>
    public DbSet<UnknownSection> UnknownSections { get; set; }
}