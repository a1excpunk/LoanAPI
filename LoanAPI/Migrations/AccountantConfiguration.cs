using LoanAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class AccountantConfiguration : IEntityTypeConfiguration<Accountant>
{
    public void Configure(EntityTypeBuilder<Accountant> builder)
    {
        builder.ToTable("Accountants");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(a => a.LastName).IsRequired().HasMaxLength(100);
        builder.Property(a => a.UserName).IsRequired().HasMaxLength(150);
        builder.Property(a => a.Password).IsRequired().HasMaxLength(200);
        builder.Property(a => a.Role).IsRequired().HasMaxLength(50);
    }
}
