using FluentValidation;
using LoanAPI.Services.DTOs;

public class LoanValidator : AbstractValidator<LoanDto>
{
    public LoanValidator()
    {
        RuleFor(x => x.LoanType)
            .IsInEnum().WithMessage("Loan type must be one of the following: QuickLoan, AutoLoan, Installment.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Loan amount must be greater than 0.")
            .LessThanOrEqualTo(1000000).WithMessage("Loan amount must be less than or equal to 1,000,000.");

        RuleFor(x => x.Currency)
            .Matches("^[A-Z]{3}$").WithMessage("Currency must be a valid 3-letter code (e.g., USD, EUR).");

        RuleFor(x => x.PeriodInMonths)
            .GreaterThan(0).WithMessage("Loan period must be between 1 and 60 months.")
            .LessThanOrEqualTo(180).WithMessage("Loan period must be between 1 and 180 months.");
    }
}
