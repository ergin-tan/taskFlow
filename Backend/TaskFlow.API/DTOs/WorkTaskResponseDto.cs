using TaskFlow.Core.Models.Enums;

namespace TaskFlow.API.DTOs
{
    public class WorkTaskResponseDto
    {
        public int Id { get; set; }
        public string TaskTitle { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int AssignedBy { get; set; }
        public string? AssignedByUserName { get; set; }
        public DateTime? DueDate { get; set; }
        public PriorityType Priority { get; set; }
        public TaskStatusType CurrentStatus { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
