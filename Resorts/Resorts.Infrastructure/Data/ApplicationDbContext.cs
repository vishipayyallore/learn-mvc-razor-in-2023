using Microsoft.EntityFrameworkCore;
using Resorts.Domain.Entities;

namespace Resorts.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Villa> Villas => Set<Villa>();
}
