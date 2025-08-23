using hahn.Domain.Entities;
using hahn.Domain.Enums;

namespace hahn.Application.DTOs
{
    public class UserDTO
    {
        public int id { get; set; }
        public string email { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string photo { get; set; } = string.Empty;
        public bool AuthCompleted { get; set; } 
        public RolesEnum role { get; set; }
    }
}