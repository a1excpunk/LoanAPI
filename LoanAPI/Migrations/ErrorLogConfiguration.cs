using LoanAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
{
    public void Configure(EntityTypeBuilder<ErrorLog> builder)
    {
        builder.ToTable("ErrorLogs");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ErrorMessage).IsRequired().HasMaxLength(500);
        builder.Property(e => e.UserId).IsRequired().HasMaxLength(50);
        builder.Property(e => e.UserRole).IsRequired().HasMaxLength(50);
        builder.Property(e => e.ErrorStatus).IsRequired().HasMaxLength(50);
        builder.Property(e => e.MethodName).IsRequired().HasMaxLength(150);
        builder.Property(e => e.InsertDate).HasDefaultValueSql("GETDATE()");
    }
}
