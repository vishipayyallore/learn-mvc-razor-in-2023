﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resorts.Domain.Entities;

public class VillaNumber
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Villa Number")]
    public int Villa_Number { get; set; }

    [ForeignKey("Villa")]
    public int VillaId { get; set; }

    // [ValidateNever]
    public Villa Villa { get; set; }

    public string? SpecialDetails { get; set; }
}