using System.ComponentModel.DataAnnotations;
using TaskFlow.Core.Models.Enums;

namespace TaskFlow.API.DTOs
{
    public class UpdateUserRequestDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(6)]
        [RegularExpression(@"^[0-9]{1,6}$", ErrorMessage = "Employee ID must consist of numbers only and be at most 6 characters long.")]
        public string? EmployeeID { get; set; }

        public string? Password { get; set; } 

        [Required]
        public TitleType Title { get; set; }

        [Required]
        public int OfficeId { get; set; }

        public bool IsActive { get; set; }
    }
}
