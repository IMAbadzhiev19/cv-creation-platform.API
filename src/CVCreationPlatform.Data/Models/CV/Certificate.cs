using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.CV;

public partial class Certificate
{
    [Key]
    public int Id { get; set; }

    public int? ResumeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CertificateName { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string? IssuingOrganization { get; set; }

    public DateTime? IssueDate { get; set; }

    [ForeignKey("ResumeId")]
    [InverseProperty("Certificates")]
    public virtual Resume? Resume { get; set; }
}
