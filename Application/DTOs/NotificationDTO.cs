using hahn.Domain.Entities;
using hahn.Domain.Enums;
using hahn.Domain.ValueObject;

namespace hahn.Application.DTOs
{
    public class NotificationDTO
    {
         public int id { get;  set; }
        public UserDTO Notifier { get; set; } = null!;
        public UserDTO Notified { get; set; } = null!;
        public string subject { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;
        public DateTime time { get; set; } 
        public bool seen { get; set; }

    }
}