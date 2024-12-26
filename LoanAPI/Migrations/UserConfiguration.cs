using LoanAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Username).IsRequired().HasMaxLength(150);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(150);
        builder.Property(u => u.Age).IsRequired();
        builder.Property(u => u.MonthlyIncome).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(u => u.IsBlocked).IsRequired();
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(200);
        builder.Property(u => u.Role).IsRequired().HasMaxLength(50);

        builder.HasMany(u => u.Loans)
            .WithOne(l => l.User)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
