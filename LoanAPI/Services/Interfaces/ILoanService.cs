using LoanAPI.Models;
using LoanAPI.Models.Enums;
using LoanAPI.Services.DTOs;

namespace LoanAPI.Interfaces
{
    public interface ILoanService
    {
        Task<LoanServiceResult> CreateLoanAsync(LoanDto dto, int userId);
        Task<List<Loan>> GetLoansByUserAsync(int userId);
        Task<Loan> GetLoanByIdAsync(int loanId, int userId);
        Task<LoanServiceResult> UpdateLoanAsync(int loanId, LoanDto dto, int userId);
        Task<LoanServiceResult> DeleteLoanAsync(int loanId, int userId);
        Task<LoanServiceResult> ChangeLoanStatusAsync(int loanId, LoanStatus status);
        Task<List<Loan>> GetAllLoansAsync();
    }
}
