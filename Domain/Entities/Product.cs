using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hahn.Domain.Enums;
using hahn.Domain.ValueObject;

namespace hahn.Domain.Entities
{
    public class Product
    {
        public int id { get; private set; }
        public int sellerId { get; private set; }

        [ForeignKey(nameof(sellerId))]
        public Seller User { get; private set; } = null!;

        [Required]
        public string name { get; private set; } = string.Empty;

        [Required]
        public string description { get; private set; } = string.Empty;

        [Required]
        public CategoryEnum category { get; private set; }

        [Required]
        public string image { get; private set; } = string.Empty;

        [Required]
        public int price { get; private set; }

        [Required]
        public int quantity { get; private set; }

        public int reviews { get; private set; } = 0;
        public DateTime addedAt { get; private set; } = DateTime.UtcNow;

        private Product() { }
        public static Product Create(
            int sellerId,
            string name,
            string description,
            CategoryEnum category,
            string image,
            int price,
            int quantity
        )
        {
            if (sellerId == null) throw new ArgumentException("Product must belong to a seller");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Product name cannot be empty");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description cannot be empty");
            if (string.IsNullOrWhiteSpace(image)) throw new ArgumentException("Product image is required");
            if (price <= 0) throw new ArgumentException("Price must be greater than zero");
            if (quantity < 0) throw new ArgumentException("Quantity cannot be negative");

            return new Product
            {
                sellerId = sellerId,
                name = name,
                description = description,
                category = category,
                image = image,
                price = price,
                quantity = quantity,
                addedAt = DateTime.UtcNow
            };
        }

        public Product Copy()
        {
            return new Product
            {
                id = this.id,
                sellerId = this.sellerId,
                User = this.User,
                name = this.name,
                description = this.description,
                category = this.category,
                image = this.image,
                price = this.price,
                quantity = this.quantity,
                reviews = this.reviews,
                addedAt = this.addedAt
            };
        }

        public void UpdateInfo(string newName, string newDescription, CategoryEnum newCategory, int newPrice)
        {
            if (string.IsNullOrWhiteSpace(newName)) throw new ArgumentException("Product name cannot be empty");
            if (string.IsNullOrWhiteSpace(newDescription)) throw new ArgumentException("Description cannot be empty");
            if (newPrice <= 0) throw new ArgumentException("Price must be greater than zero");

            name = newName;
            description = newDescription;
            category = newCategory;
            price = newPrice;
        }

        public void UpdateImage(string newImage)
        {
            if (string.IsNullOrWhiteSpace(newImage)) throw new ArgumentException("Image cannot be empty");
            image = newImage;
        }

        public void AdjustQuantity(int amount)
        {
            if (quantity + amount < 0)
                throw new InvalidOperationException("Cannot reduce quantity below zero");

            quantity += amount;
        }

        public void AddReview()
        {
            reviews++;
        }
    }
}
