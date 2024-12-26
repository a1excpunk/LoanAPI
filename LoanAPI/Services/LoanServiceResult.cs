using LoanAPI.Models;

public class LoanServiceResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Loan Loan { get; set; }
    public List<Loan> Loans { get; set; }
}
