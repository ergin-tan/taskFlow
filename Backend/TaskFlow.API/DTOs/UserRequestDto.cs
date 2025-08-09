using TaskFlow.Core.Models.Enums;

namespace TaskFlow.API.DTOs
{
    public class UserRequestDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string EmployeeID { get; set; } = string.Empty;

        public string? Password { get; set; }

        public TitleType Title { get; set; }

        public int OfficeId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
