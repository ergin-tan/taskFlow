using TaskFlow.Core.Models.Enums;

namespace TaskFlow.Core.Models
{
    public class Office : BaseEntity
    {
        public OfficeNameType OfficeName { get; set; }
        
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
