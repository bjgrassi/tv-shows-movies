using AuthService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Repositories.Mappings;

public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("tblRole"); // TODO: Specify the table name
        builder.HasKey(x => x.RoleID);
        builder.Property(x => x.RoleID).IsRequired().HasColumnName("RoleID");
        builder.Property(x => x.TypeName).IsRequired().HasColumnName("TypeName");
        builder.Property(x => x.Description).IsRequired().HasColumnName("Description");
    }
}