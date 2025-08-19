
using System;

namespace TaskFlow.API.DTOs
{
    public class AuditLogResponseDto
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public int? UserId { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string PrimaryKey { get; set; }
    }
}
