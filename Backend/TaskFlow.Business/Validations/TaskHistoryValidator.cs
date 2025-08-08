using FluentValidation;
using TaskFlow.Core.Models;
using TaskFlow.Core.Models.Enums;

namespace TaskFlow.Business.Validations
{
    public class TaskHistoryValidator : AbstractValidator<TaskHistory>
    {
        public TaskHistoryValidator()
        {
            RuleFor(history => history.WorkTaskId)
                .GreaterThan(0).WithMessage("Task ID is required.");

            RuleFor(history => history.ChangedByUserId)
                .GreaterThan(0).WithMessage("User ID is required.");

            RuleFor(history => history.NewStatus)
                .NotEmpty().WithMessage("New status is required.")
                .IsInEnum().WithMessage("Invalid new status type.");

            RuleFor(history => history.ChangeDescription)
                .MaximumLength(1000).WithMessage("Change description cannot exceed 1000 characters.");
        }
    }
}