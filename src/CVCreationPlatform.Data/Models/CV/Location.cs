using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.CV;

public partial class Location
{
    [Key]
    public int Id { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? City { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Country { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? State { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("Locations")]
    public virtual ICollection<Resume> Resumes { get; set; } = new List<Resume>();
}
