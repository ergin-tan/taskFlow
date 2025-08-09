using TaskFlow.Core.Models.Enums;

namespace TaskFlow.API.DTOs
{
    public class UpdateUserRequestDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? EmployeeID { get; set; }

        public string? Password { get; set; } 

        public TitleType Title { get; set; }

        public int OfficeId { get; set; }

        public bool IsActive { get; set; }
    }
}
