using MovieeTickets.API.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    //Relationships
    public List<ActorMovie>? ActorsMovies { get; set; }

    //Cinema
    public int CinemaId { get; set; }

    [ForeignKey("CinemaId")]
    public Cinema? Cinema { get; set; }

    //Producer
    public int ProducerId { get; set; }

    [ForeignKey("ProducerId")]
    public Producer? Producer { get; set; }
}
