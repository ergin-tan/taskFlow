using TaskFlow.Core.Models.Enums;

namespace TaskFlow.Core.Models
{
    public class WorkTask : BaseEntity
    {
        public string? TaskTitle { get; set; }

        public string? Description { get; set; }

        public int AssignedBy { get; set; }
        public DateTime? DueDate { get; set; }

        public PriorityType Priority { get; set; } = PriorityType.Medium;

        public TaskStatusType CurrentStatus { get; set; } = TaskStatusType.Assigned;

        public DateTime? CompletedAt { get; set; }

        public bool IsArchived { get; set; } = false;
        public User? AssignedByUser { get; set; }
        public ICollection<TaskHistory> History { get; set; } = new List<TaskHistory>();
        public ICollection<TaskAssignment> Assignments { get; set; } = new List<TaskAssignment>();
    }
}