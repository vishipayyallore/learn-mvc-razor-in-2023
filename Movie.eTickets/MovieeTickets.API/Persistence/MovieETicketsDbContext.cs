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

    public DbSet<ActorMovie> ActorsMovies => Set<ActorMovie>();

    public DbSet<Cinema> Cinemas => Set<Cinema>();

    public DbSet<Producer> Prducers => Set<Producer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActorMovie>().HasKey(am => new
        {
            am.ActorId,
            am.MovieId
        });

        modelBuilder.Entity<ActorMovie>().HasOne(m => m.Movie).WithMany(am => am.ActorsMovies).HasForeignKey(m => m.MovieId);

        modelBuilder.Entity<ActorMovie>().HasOne(m => m.Actor).WithMany(am => am.ActorsMovies).HasForeignKey(m => m.ActorId);

        base.OnModelCreating(modelBuilder);
    }

}
