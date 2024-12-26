using LoanAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace LoanAPI.Services.DTOs
{
    public class LoanDto
    {
        [Required]
        public LoanType LoanType { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public int PeriodInMonths { get; set; }
    }
}
