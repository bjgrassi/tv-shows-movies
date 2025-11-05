using AuthService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Repositories.Mappings;

public class AccountMapping : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("tblAccount"); // TODO: Specify the table name
        builder.HasKey(x => x.Email);
        builder.Property(x => x.Email).IsRequired().HasColumnName("Email");
        builder.Property(x => x.FullName).IsRequired().HasColumnName("FullName");
        builder.Property(x => x.Password).IsRequired().HasColumnName("Password");
    }
}