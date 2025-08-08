using System.ComponentModel.DataAnnotations;
using TaskFlow.Core.Models.Enums;

namespace TaskFlow.API.DTOs
{
    public class OfficeRequestDto
    {
        [Required]
        public OfficeNameType OfficeName { get; set; }
    }
}
