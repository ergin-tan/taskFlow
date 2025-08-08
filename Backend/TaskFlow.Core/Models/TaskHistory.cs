using System.ComponentModel.DataAnnotations.Schema;
using TaskFlow.Core.Models.Enums;

namespace TaskFlow.Core.Models;
public class TaskHistory : BaseEntity
{
    [ForeignKey("WorkTask")]
    public int WorkTaskId { get; set; }

    [ForeignKey("ChangedByUser")]
    public int ChangedByUserId { get; set; }

    public TaskStatusType? OldStatus { get; set; }

    public TaskStatusType NewStatus { get; set; }

    public string? ChangeDescription { get; set; }

    public WorkTask? WorkTask { get; set; }
    public User? ChangedByUser { get; set; }
}
