using FluentValidation;
using LoanAPI.Models;

namespace LoanAPI.Validators
{
    public class AccountantValidator : AbstractValidator<Accountant>
    {

        public AccountantValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(1, 50).WithMessage("First name length should be between 1 and 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(1, 50).WithMessage("Last name length should be between 1 and 50 characters.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .Length(3, 50).WithMessage("Username length should be between 3 and 50 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(7).WithMessage("Password length should be at least 7 characters.")
                .MaximumLength(25).WithMessage("Password length should be at most 25 characters.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("The Role field is empty")
                .Matches("^(Accountant|User)$").WithMessage("Role must be one of the following: Accountant, User");
        }

    }
}
