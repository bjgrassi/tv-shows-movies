using ContentService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentService.Repositories.Mappings;
public class GenreMovieMapping : IEntityTypeConfiguration<GenreMovie>
{
    public void Configure(EntityTypeBuilder<GenreMovie> builder)
    {
        builder.ToTable("tblGenreMovie");
        builder.HasKey(x => new { x.GenreID, x.MovieID });
        builder.Property(x => x.GenreID).IsRequired();
        builder.Property(x => x.MovieID).IsRequired();
    }
}