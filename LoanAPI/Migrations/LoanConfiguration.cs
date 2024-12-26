using LoanAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.ToTable("Loans");
        builder.HasKey(l => l.LoanId);

        builder.Property(l => l.Amount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(l => l.Currency).IsRequired().HasMaxLength(3); 
        builder.Property(l => l.PeriodInMonths).IsRequired();

        builder.Property(l => l.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne<User>()
            .WithMany(u => u.Loans)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
