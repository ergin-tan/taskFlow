using TaskFlow.Core.Models.Enums;

namespace TaskFlow.API.DTOs
{
    public class TaskHistoryRequestDto
    {
        public int WorkTaskId { get; set; }

        public int ChangedByUserId { get; set; }

        public TaskStatusType? OldStatus { get; set; }

        public TaskStatusType NewStatus { get; set; }

        public string? ChangeDescription { get; set; }
    }
}
