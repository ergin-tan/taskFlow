using TaskFlow.Core.Models.Enums;

namespace TaskFlow.API.DTOs
{
    public class TaskAssignmentRequestDto
    {
        public int WorkTaskId { get; set; }

        public int AssignedTo { get; set; }

        public string AssignedPart { get; set; } = string.Empty;

        public AssignmentStatusType Status { get; set; }
        public DateTime? DueDate { get; set; }

        public DateTime? CompletedAt { get; set; }

        public string? Remarks { get; set; }
    }
}
