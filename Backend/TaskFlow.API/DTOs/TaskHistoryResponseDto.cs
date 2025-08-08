using TaskFlow.Core.Models.Enums;

namespace TaskFlow.API.DTOs
{
    public class TaskHistoryResponseDto
    {
        public int Id { get; set; }
        public int WorkTaskId { get; set; }
        public string WorkTaskTitle { get; set; } = string.Empty;
        public int ChangedByUserId { get; set; }
        public string ChangedByUserName { get; set; } = string.Empty;
        public TaskStatusType? OldStatus { get; set; }
        public TaskStatusType NewStatus { get; set; }
        public string? ChangeDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
