using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using hahn.Domain.Enums;
using hahn.Domain.ValueObject;

namespace hahn.Domain.Entities
{
    public class Order
    {
        public int id { get; set; }

        public int buyerId { get; set; } 
        [ForeignKey(nameof(buyerId))]
        public Buyer Buyer { get; set; } = null!;


        public int productId { get; set; } 

        [ForeignKey(nameof(productId))]   
        public Product Product { get; set; } = null!;
        public int quantity { get; set; }
        public OrderState state { get; set; } = OrderState.PENDING;
        public DateTime addedAt { get; set; } = DateTime.Now;
    }
    
  
}