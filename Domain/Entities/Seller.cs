using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using hahn.Domain.Enums;
using hahn.Domain.ValueObject;

namespace hahn.Domain.Entities
{
    public class Seller
    {
        public int id { get; set; }

        public int userId { get; set; }

        [ForeignKey(nameof(userId))]
        public User User { get; set; } = null!;

        [Required]
        [NotNull]
        public string shopName { get; set; } = string.Empty;

        public string shopLogo { get; set; } = string.Empty;

        [Required]
        [NotNull]
        public string shopeDescription { get; set; } = string.Empty;

        [Required]
        [NotNull]
        public Adress adress { get; set; } = null!;

        [Required]
        public Boolean hasLocal { get; set; } = false;

        public Adress localAdress { get; set; } = null!;

        [Required]
        [NotNull]
        public string field { get; set; } = string.Empty;


        public string? personalSite { get; set; }
        public string? facebook { get; set; }
        public string? instagram { get; set; }
        public int rating { get; set; } = 0;
        public Sources mySource { get; set; } = Sources.Other;
        public DateTime joinedAt { get; set; } = DateTime.Now;


        public static Seller Create(
            int userId,
            string shopName,
            string shopDescription,
            Adress adress,
            string field,
            bool hasLocal = false,
            Adress? localAdress = null,
            string? shopLogo = null,
            string? personalSite = null,
            string? facebook = null,
            string? instagram = null,
            Sources mySource = Sources.Other,
            int rating = 0
        )
        {
            if (userId == null) throw new ArgumentException("Seller must be associated with a user");
            if (string.IsNullOrWhiteSpace(shopName)) throw new ArgumentException("Shop name cannot be empty");
            if (string.IsNullOrWhiteSpace(shopDescription)) throw new ArgumentException("Shop description cannot be empty");
            if (adress == null) throw new ArgumentException("Adress cannot be null");
            if (string.IsNullOrWhiteSpace(field)) throw new ArgumentException("Field cannot be empty");

            return new Seller
            {
                userId = userId,
                shopName = shopName,
                shopeDescription = shopDescription,
                adress = adress,
                field = field,
                hasLocal = hasLocal,
                localAdress = localAdress ?? new Adress(),
                shopLogo = shopLogo ?? "default.png",
                personalSite = personalSite,
                facebook = facebook,
                instagram = instagram,
                mySource = mySource,
                rating = rating,
                joinedAt = DateTime.UtcNow
            };
        }

    }


}