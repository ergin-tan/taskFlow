using System.ComponentModel.DataAnnotations;
using TaskFlow.Core.Models.Enums;

namespace TaskFlow.API.DTOs
{
    public class WorkTaskRequestDto
    {
        [Required]
        [StringLength(255)]
        public string TaskTitle { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public int AssignedBy { get; set; }
        public DateTime? DueDate { get; set; }

        [Required]
        public PriorityType Priority { get; set; }

        [Required]
        public TaskStatusType CurrentStatus { get; set; }

        public DateTime? CompletedAt { get; set; }

        public bool IsArchived { get; set; } = false;
    }
}
