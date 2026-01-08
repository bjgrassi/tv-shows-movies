using ContentService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentService.Repositories.Mappings;
public class SerieMapping : IEntityTypeConfiguration<Serie>
{
    public void Configure(EntityTypeBuilder<Serie> builder)
    {
        builder.ToTable("tblSerie");
        builder.HasKey(x => x.SerieID);
        builder.Property(x => x.SerieID).IsRequired();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Synopsis);
        builder.Property(x => x.ImageUrl).HasMaxLength(500);
        builder.Property(x => x.TypeName).HasDefaultValue("Serie").HasMaxLength(5);
        builder.Property(x => x.NumOfSeasons);
        builder.Property(x => x.IsFinished);
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
        builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETDATE()");
    }
}