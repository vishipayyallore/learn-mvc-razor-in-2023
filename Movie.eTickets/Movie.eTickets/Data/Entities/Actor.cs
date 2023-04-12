using System.ComponentModel.DataAnnotations;

namespace Movie.eTickets.Data.Entities;

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
}
