using LoanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanAPI.Migrations
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AccountantConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorLogConfiguration());
            modelBuilder.ApplyConfiguration(new LoanConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }

}
