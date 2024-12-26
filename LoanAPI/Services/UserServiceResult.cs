using LoanAPI.Models;
using LoanAPI.Models.Enums;

public class UserServiceResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public User User { get; set; }
    public string Role { get; set; }
}