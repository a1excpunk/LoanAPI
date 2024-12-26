using LoanAPI.Models.Enums;

namespace LoanAPI.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public LoanType LoanType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int PeriodInMonths { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.InProgress;
        public int UserId { get; set; }

        public User User { get; set; }
    }

}
