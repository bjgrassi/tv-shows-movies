using ContentController.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentController.Repositories.Mappings;
public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("tblMovie");
        builder.HasKey(x => x.MovieID);
        builder.Property(x => x.MovieID).IsRequired();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Synopsis);
        builder.Property(x => x.ImageUrl).HasMaxLength(500);
        builder.Property(x => x.ReleaseYear);
        builder.Property(x => x.TypeName).HasDefaultValue("Movie").HasMaxLength(5);
        builder.Property(x => x.RunningTime);
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
        builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETDATE()");
    }
}