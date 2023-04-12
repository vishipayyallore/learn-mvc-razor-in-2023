using Microsoft.EntityFrameworkCore;
using MovieeTickets.API.Data.Entities;

namespace MovieeTickets.API.Persistence;

public class MovieETicketsDbContext : DbContext
{
    public MovieETicketsDbContext(DbContextOptions<MovieETicketsDbContext> options) : base(options)
    {
    }

    public DbSet<Actor> Actors => Set<Actor>();

    public DbSet<Movie> Movies => Set<Movie>();

    // public DbSet<Actor_Movie> Actors_Movies { get; set; }

    public DbSet<Cinema> Cinemas => Set<Cinema>();

    public DbSet<Producer> Prducers => Set<Producer>();

}
