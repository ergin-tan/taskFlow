using System.ComponentModel.DataAnnotations;
using TaskFlow.Core.Models.Enums;

namespace TaskFlow.API.DTOs
{
    public class TaskAssignmentRequestDto
    {
        [Required]
        public int WorkTaskId { get; set; }

        [Required]
        public int AssignedTo { get; set; }

        [StringLength(255)]
        public string AssignedPart { get; set; } = string.Empty;

        [Required]
        public AssignmentStatusType Status { get; set; }
        public DateTime? DueDate { get; set; }

        public DateTime? CompletedAt { get; set; }

        public string? Remarks { get; set; }
    }
}
