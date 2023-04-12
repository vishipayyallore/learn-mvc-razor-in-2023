using System.ComponentModel.DataAnnotations;

namespace MovieeTickets.API.Data.Entities;

public class Actor
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? ProfilePictureURL { get; set; }

    [Required]
    public string? FullName { get; set; }

    [Required]
    public string? Bio { get; set; }

    //Relationships
    public List<ActorMovie>? ActorsMovies { get; set; }
}
