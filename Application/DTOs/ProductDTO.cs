using hahn.Domain.Entities;
using hahn.Domain.Enums;

namespace hahn.Application.DTOs
{
    public class ProductDTO
    {
        public int id { get; set; }

        public int sellerId { get; set; }

        public string name { get; set; } = string.Empty;

        public SellerDTO? Seller { get; set; }

        public string description { get; set; } = string.Empty;

        public CategoryEnum category { get; set; }
        public string image { get; set; } = string.Empty;
        public int price { get; set; }
        public int quantity { get; set; }
        public int reviews { get; set; } = 0;
        public DateTime addedAt { get; set; } = DateTime.Now;
        

       
    }
}