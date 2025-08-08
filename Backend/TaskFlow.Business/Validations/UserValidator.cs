using FluentValidation;
using TaskFlow.Core.Models;

namespace TaskFlow.Business.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("First name cannot be empty.")
                .Length(1, 50).WithMessage("First name must be between 1 and 50 characters.");

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage("Last name cannot be empty.")
                .Length(1, 50).WithMessage("Last name must be between 1 and 50 characters.");

            RuleFor(user => user.EmployeeID)
                .NotEmpty().WithMessage("Employee ID cannot be empty.")
                .Length(1, 6).WithMessage("Employee ID must be at most 6 characters long.")
                .Matches("^[0-9]{1,6}$").WithMessage("Employee ID must consist of numbers only.");
            
            RuleFor(user => user.PasswordHash)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .Length(1, 255).WithMessage("Password length is invalid.");

            RuleFor(user => user.OfficeId)
                .GreaterThan(0).WithMessage("Office must be specified.");
        }
    }
}