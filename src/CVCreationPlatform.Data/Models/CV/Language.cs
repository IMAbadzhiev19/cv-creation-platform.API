using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Models.CV
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        [Unicode(false)]
        public string? Name { get; set; }
        
        [Unicode(false)]
        public string? Level { get; set; }

        public Guid? ResumeId { get; set; }

        [ForeignKey("ResumeId")]
        public virtual Resume? Resume { get; set; }
    }
}
