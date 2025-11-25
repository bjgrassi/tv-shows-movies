using Microsoft.EntityFrameworkCore;

namespace MovieService.Repositories;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}