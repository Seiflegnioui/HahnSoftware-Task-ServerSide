
using hahn.Application.DTOs;
using hahn.Domain.Entities;

namespace hahn.Infrastructure.Services
{
    public class ProductMapper()
    {
        public static ProductDTO ToDTO(Product p)
        {
            return new ProductDTO
            {
                id = p.id,
                sellerId = p.sellerId,
                name = p.name,
                description = p.description,
                category = p.category,
                image = p.image,
                price = p.price,
                quantity = p.quantity,
                reviews = p.reviews,
                addedAt = p.addedAt,
                Seller = new SellerDTO
                {
                    id = p.User.id,
                    userId = p.User.userId,
                    shopName = p.User.shopName,
                    shopLogo = p.User.shopLogo,
                    shopeDescription = p.User.shopeDescription,
                    field = p.User.field,
                    adress = p.User.adress,
                    localAdress = p.User.localAdress,
                    hasLocal = p.User.hasLocal,
                    personalSite = p.User.personalSite,
                    facebook = p.User.facebook,
                    instagram = p.User.instagram,
                    rating = p.User.rating,
                    mySource = p.User.mySource,
                    joinedAt = p.User.joinedAt,

                    email = p.User.User.email,
                    username = p.User.User.username,
                    photo = p.User.User.photo
                }
            };
        }

        public static List<ProductDTO> ToDTOList(List<Product> products)
        {
            return products.Select(p => ToDTO(p)).ToList();
        }
    }
}