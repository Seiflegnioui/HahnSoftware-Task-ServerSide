using hahn.Domain.Entities;

namespace hahn.Application.DTOs
{
    public class UserDTO
    {
        public int id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string phone { get; set; }
        public string photo { get; set; }
        public Roles role { get; set; }
    }
}