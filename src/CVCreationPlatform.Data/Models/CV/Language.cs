using Data.Models.CV;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.Data.Models.CV
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        [Unicode(false)]
        public string? Name { get; set; }
        
        [Unicode(false)]
        public string? Level { get; set; }

        public int? ResumeId { get; set; }

        [ForeignKey("ResumeId")]
        [InverseProperty("Languages")]
        public virtual Resume? Resume { get; set; }
    }
}
