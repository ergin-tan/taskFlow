using TaskFlow.Core.Models.Enums;

namespace TaskFlow.API.DTOs
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmployeeID { get; set; } = string.Empty;
        public TitleType Title { get; set; }
        public int OfficeId { get; set; }
        public string? OfficeName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
