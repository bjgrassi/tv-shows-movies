using ContentService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentService.Repositories.Mappings;
public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("tblGenre");
        builder.HasKey(x => x.GenreID);
        builder.Property(x => x.GenreID).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
    }
}