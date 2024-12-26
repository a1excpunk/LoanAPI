using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanAPI.Models;
using LoanAPI.Interfaces;
using LoanAPI.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using LoanAPI.Models.Enums;
using LoanAPI.Migrations;
using FluentValidation;

public class LoanService : ILoanService
{
    private readonly AppDbContext _context;
    private readonly IValidator<Loan> _loanValidator;
    public LoanService(AppDbContext context, IValidator<Loan> loanvalidator)
    {
        _context = context;
        _loanValidator = loanvalidator;
    }

    public async Task<LoanServiceResult> CreateLoanAsync(LoanDto dto, int userId)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return new LoanServiceResult { Success = false, Message = "User not found." };

        if (user.IsBlocked)
            return new LoanServiceResult { Success = false, Message = "User is blocked and cannot request loans." };

        var loan = new Loan
        {
            LoanType = dto.LoanType,
            Amount = dto.Amount,
            Currency = dto.Currency,
            PeriodInMonths = dto.PeriodInMonths,
            Status = LoanStatus.InProgress,
            UserId = userId
        };

        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();

        return new LoanServiceResult { Success = true, Message = "Loan created successfully.", Loan = loan };
    }

    public async Task<List<Loan>> GetLoansByUserAsync(int userId)
    {
        var loans = await _context.Loans.Where(l => l.UserId == userId).ToListAsync();

        if (!loans.Any())
            throw new Exception("No loans found for the user.");

        return loans;
    }

    public async Task<Loan> GetLoanByIdAsync(int loanId, int userId)
    {
        var loan = await _context.Loans
            .FirstOrDefaultAsync(l => l.LoanId == loanId && l.UserId == userId);

        if (loan == null)
            throw new Exception("Loan not found.");

        return loan;
    }

    public async Task<LoanServiceResult> UpdateLoanAsync(int loanId, LoanDto dto, int userId)
    {
        var loan = await _context.Loans.SingleOrDefaultAsync(l => l.LoanId == loanId && l.UserId == userId);

        if (loan == null)
            return new LoanServiceResult { Success = false, Message = "Loan not found." };

        if (loan.Status != LoanStatus.InProgress)
            return new LoanServiceResult { Success = false, Message = "Only loans with 'InProgress' status can be updated." };

        loan.Amount = dto.Amount;
        loan.PeriodInMonths = dto.PeriodInMonths;
        loan.Currency = dto.Currency;

        _context.Loans.Update(loan);
        await _context.SaveChangesAsync();

        return new LoanServiceResult { Success = true, Message = "Loan updated successfully.", Loan = loan };
    }

    public async Task<LoanServiceResult> DeleteLoanAsync(int loanId, int userId)
    {
        var loan = await _context.Loans.SingleOrDefaultAsync(l => l.LoanId == loanId && l.UserId == userId);

        if (loan == null)
            return new LoanServiceResult { Success = false, Message = "Loan not found." };

        if (loan.Status != LoanStatus.InProgress)
            return new LoanServiceResult { Success = false, Message = "Only loans with 'InProgress' status can be deleted." };

        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync();

        return new LoanServiceResult { Success = true, Message = "Loan deleted successfully." };
    }

    public async Task<LoanServiceResult> ChangeLoanStatusAsync(int loanId, LoanStatus status)
    {
        var loan = await _context.Loans.SingleOrDefaultAsync(l => l.LoanId == loanId);

        if (loan == null)
            return new LoanServiceResult { Success = false, Message = "Loan not found." };

        loan.Status = status;
        _context.Loans.Update(loan);
        await _context.SaveChangesAsync();

        return new LoanServiceResult { Success = true, Message = "Loan status updated successfully.", Loan = loan };
    }

    public async Task<List<Loan>> GetAllLoansAsync()
    {
        return await _context.Loans.ToListAsync();
    }
}
