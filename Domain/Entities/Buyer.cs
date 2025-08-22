using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using hahn.Domain.ValueObject;

namespace hahn.Domain.Entities
{
    public class Buyer
    {
        public int id { get; set; }

        public int userId { get; set; } 

        [ForeignKey(nameof(userId))]
        public User User { get; set; } = null!;

        [Required]
        [NotNull]
        public DateTime brthdate { get; set; } 
        
        [Required]
        public Adress adress { get; set; } = null!;


        public string? bio { get; set; } = string.Empty;

        public Sources mySource { get; set; } = Sources.Other;
        public DateTime joinedAt { get; set; } = DateTime.Now;

    }
    
  
}