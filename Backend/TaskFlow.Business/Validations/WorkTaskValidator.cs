using FluentValidation;
using TaskFlow.Core.Models;
using System;

namespace TaskFlow.Business.Validations
{
    public class WorkTaskValidator : AbstractValidator<WorkTask>
    {
        public WorkTaskValidator()
        {
            RuleFor(task => task.TaskTitle)
                .NotEmpty().WithMessage("Task title is required.")
                .Length(1, 255).WithMessage("Task title must be between 1 and 255 characters.");

            RuleFor(task => task.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(task => task.AssignedBy)
                .GreaterThan(0).WithMessage("A task must be assigned by a valid user.");

            RuleFor(task => task.DueDate)
                .Must(date => date == null || date.Value.Date >= DateTime.Today)
                .WithMessage("Due date cannot be in the past.");
            
            RuleFor(task => task.CompletedAt)
                .Must(date => date == null || date.Value.Date >= DateTime.Today)
                .WithMessage("Completion date cannot be in the past.");

            RuleFor(task => task.Priority)
                .IsInEnum().WithMessage("Invalid priority type.");
            
            RuleFor(task => task.CurrentStatus)
                .IsInEnum().WithMessage("Invalid task status type.");
        }
    }
}