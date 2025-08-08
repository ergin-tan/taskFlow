using System.ComponentModel.DataAnnotations;
using TaskFlow.Core.Models.Enums;

namespace TaskFlow.API.DTOs
{
    public class TaskHistoryRequestDto
    {
        [Required]
        public int WorkTaskId { get; set; }

        [Required]
        public int ChangedByUserId { get; set; }

        public TaskStatusType? OldStatus { get; set; }

        [Required]
        public TaskStatusType NewStatus { get; set; }

        public string? ChangeDescription { get; set; }
    }
}
