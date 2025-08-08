using FluentValidation;
using TaskFlow.Core.Models;
using System;

namespace TaskFlow.Business.Validations
{
    public class TaskAssignmentValidator : AbstractValidator<TaskAssignment>
    {
        public TaskAssignmentValidator()
        {
            RuleFor(assignment => assignment.WorkTaskId)
                .GreaterThan(0).WithMessage("Task ID is required.");
            
            RuleFor(assignment => assignment.AssignedTo)
                .GreaterThan(0).WithMessage("User ID is required.");

            RuleFor(assignment => assignment.AssignedPart)
                .MaximumLength(255).WithMessage("Assigned part cannot exceed 255 characters.");

            RuleFor(assignment => assignment.Status)
                .NotEmpty().WithMessage("Status is required.")
                .IsInEnum().WithMessage("Invalid assignment status type.");

            RuleFor(assignment => assignment.DueDate)
                .Must(date => date == null || date.Value.Date >= DateTime.Today)
                .WithMessage("Due date cannot be in the past.");
            
            RuleFor(assignment => assignment.CompletedAt)
                .Must(date => date == null || date.Value.Date >= DateTime.Today)
                .WithMessage("Completion date cannot be in the past.");
            
            RuleFor(assignment => assignment.Remarks)
                .MaximumLength(1000).WithMessage("Remarks cannot exceed 1000 characters.");
        }
    }
}