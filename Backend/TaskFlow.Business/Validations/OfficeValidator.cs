using FluentValidation;
using TaskFlow.Core.Models;
using TaskFlow.Core.Models.Enums;

namespace TaskFlow.Business.Validations
{
    public class OfficeValidator : AbstractValidator<Office>
    {
        public OfficeValidator()
        {
            RuleFor(office => office.OfficeName)
                .NotEmpty().WithMessage("Office name is required.")
                .IsInEnum().WithMessage("Invalid office name type.");
        }
    }
}