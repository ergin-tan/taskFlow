using TaskFlow.Core.Models.Enums;

namespace TaskFlow.API.DTOs
{
    public class TaskAssignmentResponseDto
    {
        public int Id { get; set; }
        public int WorkTaskId { get; set; }
        public string WorkTaskTitle { get; set; } = string.Empty;
        public int AssignedTo { get; set; }
        public string AssignedToUserName { get; set; } = string.Empty;
        public string AssignedPart { get; set; } = string.Empty;
        public AssignmentStatusType Status { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string? Remarks { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
