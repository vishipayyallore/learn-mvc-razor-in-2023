using MovieeTickets.API.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MovieeTickets.API.Data.Entities;

public class Movie
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public double? Price { get; set; }

    [Required]
    public string? ImageURL { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public MovieCategory MovieCategory { get; set; }
}
