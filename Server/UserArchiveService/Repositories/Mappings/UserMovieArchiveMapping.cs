using UserArchiveService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserArchiveService.Repositories.Mappings;
public class UserMovieArchiveMapping : IEntityTypeConfiguration<UserMovieArchive>
{
    public void Configure(EntityTypeBuilder<UserMovieArchive> builder)
    {
        builder.ToTable("tblUserMovieArchive");
        builder.HasKey(x => x.UserMovieArchiveID);
        builder.Property(x => x.IsWatchLater);
        builder.Property(x => x.IsWatched);
        builder.HasOne(u => u.Movie)
           .WithMany()
           .HasForeignKey(u => u.MovieFK);
        builder.HasOne(u => u.UserAccount)
           .WithMany()
           .HasForeignKey(u => u.UserAccountFK);
    }
}