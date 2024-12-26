using FluentValidation;
using LoanAPI.Models;

namespace LoanAPI.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(1, 50).WithMessage("First name length should be between 1 and 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(1, 50).WithMessage("Last name length should be between 1 and 50 characters.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .Length(3, 50).WithMessage("Username length should be between 3 and 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(7).WithMessage("Password length should be at least 7 characters.")
                .MaximumLength(25).WithMessage("Password length should be at most 25 characters.");

            RuleFor(x => x.Age)
                .GreaterThan(18).WithMessage("Age must be greater than 18.");

            RuleFor(x => x.MonthlyIncome)
                .GreaterThan(0).WithMessage("Monthly income must be greater than 0.");
        }
    }
}
