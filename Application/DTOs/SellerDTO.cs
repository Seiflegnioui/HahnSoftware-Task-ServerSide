using hahn.Domain.Entities;
using hahn.Domain.Enums;
using hahn.Domain.ValueObject;

namespace hahn.Application.DTOs
{
    public class SellerDTO
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string shopName { get; set; } = string.Empty;
        public string shopLogo { get; set; }  = string.Empty;
        public string shopeDescription { get; set; }  = string.Empty;
        public Adress adress { get; set; } = null!;
        public Boolean hasLocal { get; set; } = false;
        public Adress localAdress { get; set; } = null!;
        public string field { get; set; } = string.Empty;
        public string? personalSite { get; set; }
        public string? facebook { get; set; }
        public string? instagram { get; set; }
        public int rating { get; set; } = 0;
        public Sources mySource { get; set; } = Sources.Other;
        public DateTime joinedAt { get; set; } = DateTime.Now;

        public string? username { get; set; }
        public string? email { get; set; }
        public string? photo { get; set; }

        public static implicit operator SellerDTO(UserDTO v)
        {
            throw new NotImplementedException();
        }
    }
}