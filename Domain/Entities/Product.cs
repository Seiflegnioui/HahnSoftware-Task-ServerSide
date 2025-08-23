using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using hahn.Domain.Enums;
using hahn.Domain.ValueObject;

namespace hahn.Domain.Entities
{
    public class Product
    {
        public int id { get; set; }

        public int sellerId { get; set; } 

        [ForeignKey(nameof(sellerId))]
        public Seller User { get; set; } = null!;

        [Required]
        public string name { get; set; } = string.Empty;
        
        [Required]
        public string description { get; set; } = string.Empty;

        [Required]
        public CategoryEnum category { get; set; }
        [Required]
        public string image { get; set; } = string.Empty;
        [Required]
        public int price { get; set; } 
        [Required]
        public int quantity { get; set; } 
        public int reviews { get; set; } = 0;
        public DateTime addedAt { get; set; } = DateTime.Now;

    }
    
  
}