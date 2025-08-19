
using System;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Core.Models
{
    public class AuditLog : BaseEntity
    {
        [Required]
        public string TableName { get; set; }

        [Required]
        public string Action { get; set; }

        public DateTime Timestamp { get; set; }

        public int? UserId { get; set; }

        public string OldValues { get; set; }

        public string NewValues { get; set; }

        [Required]
        public string PrimaryKey { get; set; }
    }
}
