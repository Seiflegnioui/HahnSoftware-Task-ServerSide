using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using hahn.Domain.ValueObject;

namespace hahn.Domain.Entities
{
    public class Seller
    {
        
        public int id { get; set; }
        [ForeignKey("userId")]
        public int userId { get; set; }

        [Required]
        [NotNull]
        public string shopName { get; set; } = string.Empty;

        [Required]
        [NotNull]
        public string shopLogo { get; set; }  = string.Empty;

        [Required]
        [NotNull]
        public string shopeDescription { get; set; }  = string.Empty;

        public Adress adress { get; set; } = null!;

        [Required]
        [NotNull]
        public Boolean hasLocal { get; set; } = false;


        public string localAdress { get; set; } = string.Empty;

        [Required]
        [NotNull]
        public string field { get; set; }  = string.Empty;

        [Required]
        [NotNull]

        public string? personalSite { get; set; }
        public string? facebook { get; set; }
        public string? instagram { get; set; }
        public int rating { get; set; } = 0;
        public Sources mySource { get; set; } = Sources.Other;
        public DateTime joinedAt { get; set; } = DateTime.Now;

    }
    
    public enum Sources
    {
        Instagram, Facebook, Freind, X, Other
    }
}