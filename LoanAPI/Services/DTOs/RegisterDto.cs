using System.ComponentModel.DataAnnotations;

namespace LoanAPI.Services.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public decimal MonthlyIncome { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
