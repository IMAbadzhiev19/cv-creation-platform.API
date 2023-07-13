using Data.Models.CV;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVCreationPlatform.Data.Models.CV;

public class UnknownSection
{
    [Key]
    public int Id { get; set; }

    [Unicode(false)]
    public string? Title { get; set; }

    [Unicode(false)]
    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set;}


    public Guid? ResumeId { get; set; }

    [ForeignKey("ResumeId")]
    public virtual Resume? Resume { get; set; }
}
