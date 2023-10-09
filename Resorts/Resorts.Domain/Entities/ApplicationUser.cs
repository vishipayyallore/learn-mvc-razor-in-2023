using Microsoft.AspNetCore.Identity;

namespace Resorts.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public required string Name { get; set; }

    public DateTime CreatedAt { get; set; }
}