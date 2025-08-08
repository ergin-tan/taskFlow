using TaskFlow.Core.Models.Enums;

namespace TaskFlow.API.DTOs
{
    public class OfficeResponseDto
    {
        public int Id { get; set; }
        public OfficeNameType OfficeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
