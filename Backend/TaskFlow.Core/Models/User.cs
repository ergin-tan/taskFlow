using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskFlow.Core.Models.Enums;

namespace TaskFlow.Core.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? EmployeeID { get; set; }

        public string? PasswordHash { get; set; }

        public TitleType Title { get; set; }
        public int OfficeId { get; set; }

        public bool IsActive { get; set; } = true;
        public Office? Office { get; set; }

        public ICollection<TaskHistory> TaskHistoryEntries { get; set; } = new List<TaskHistory>();
    }
}