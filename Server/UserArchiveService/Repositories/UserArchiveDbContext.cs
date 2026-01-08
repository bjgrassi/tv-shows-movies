using ContentService.Domain;
using ContentService.Repositories.Mappings;
using Microsoft.EntityFrameworkCore;

namespace UserArchiveService.Repositories;

public class UserArchiveDbContext : DbContext
{
    public UserArchiveDbContext(DbContextOptions<UserArchiveDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserArchiveDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}