using hahn.Domain.Entities;
using hahn.Domain.Enums;
using hahn.Domain.ValueObject;

namespace hahn.Application.DTOs
{
    public class OrderDTO
    {
         public int id { get; set; }
        public BuyerDTO Buyer { get; set; }  = null!;
        public ProductDTO Product { get; set; } = null!;
        public int quantity { get; set; }
        public OrderState state { get; set; } 
        public DateTime addedAt { get; set; } 

    }
}