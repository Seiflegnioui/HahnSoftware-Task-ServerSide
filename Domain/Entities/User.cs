using System.Runtime.CompilerServices;
using hahn.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace hahn.Domain.Entities
{
    [Index("username", IsUnique = true)]
    [Index("email", IsUnique = true)]

    public class User
    {
        public int id { get; set; }
        public string email { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string hashedPpassword { get; set; } = string.Empty;
        public RolesEnum role { get; set; }
        public bool AuthCompleted { get; set; } = false;
        public string? photo { get; set; }
        public DateTime joinedAt { get; set; } = DateTime.Now;

    }
    

 
}