using hahn.Domain.Entities;
using hahn.Domain.Enums;
using hahn.Domain.ValueObject;

namespace hahn.Application.DTOs
{
    public class BuyerDTO
    {
        public int id { get; set; }
        public int userId { get; set; }
        public DateTime brthdate { get; set; }
        public Adress adress { get; set; } = null!;
        public string bio { get; set; } = string.Empty;
        public Sources mySource { get; set; }
        public DateTime joinedAt { get; set; }

        public string? username { get; set; } = null!;
        public string? email { get; set; }
        public string? photo { get; set; }
    }
}