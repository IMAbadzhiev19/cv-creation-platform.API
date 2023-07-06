using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.CV;

[Keyless]
public partial class ResumeTemplate
{
    public int? ResumeId { get; set; }

    public int? TemplateId { get; set; }

    [ForeignKey("ResumeId")]
    public virtual Resume? Resume { get; set; }

    [ForeignKey("TemplateId")]
    public virtual Template? Template { get; set; }
}
