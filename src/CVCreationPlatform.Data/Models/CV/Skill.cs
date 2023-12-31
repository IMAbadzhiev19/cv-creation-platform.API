﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.CV;

public partial class Skill
{
    [Key]
    public int Id { get; set; }

    public string? SkillName { get; set; }

    [ForeignKey("SkillId")]
    public virtual ICollection<Resume> Resumes { get; set; } = new List<Resume>();
}
