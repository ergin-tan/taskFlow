using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskFlow.Core.Models.Enums;

namespace TaskFlow.Core.Models;

public class TaskAssignment: BaseEntity
{
    public int WorkTaskId { get; set; }

    public int AssignedTo { get; set; }

    public string? AssignedPart { get; set; }

    public AssignmentStatusType Status { get; set; } = AssignmentStatusType.Pending;
    public DateTime? DueDate { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? Remarks { get; set; }

    // Navigations
    public WorkTask? WorkTask { get; set; }
    public User? AssignedToUser { get; set; }
}
