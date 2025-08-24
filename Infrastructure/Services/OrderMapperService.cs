using hahn.Application.DTOs;
using hahn.Domain.Entities;

namespace hahn.Infrastructure.Services
{
    public class OrderMapper
    {
        public static OrderDTO ToDTO(Order o)
        {
            return new OrderDTO
            {
                id = o.id,
                quantity = o.quantity,
                state = o.state,
                addedAt = o.addedAt,
                Buyer = new BuyerDTO
                {
                    id = o.Buyer.id,
                    adress = o.Buyer.adress,
                    joinedAt = o.Buyer.joinedAt,
                    username = o.Buyer.User.username,
                    email = o.Buyer.User.email,
                    photo = o.Buyer.User.photo
                },
                Product = new ProductDTO
                {
                    id = o.Product.id,
                    name = o.Product.name,
                    image = o.Product.image,
                    price = o.Product.price,
                    sellerId = o.Product.sellerId,
                    Seller = new SellerDTO
                    {
                        id = o.Product.User.id,
                        userId = o.Product.User.userId,
                        shopName = o.Product.User.shopName,
                        shopLogo = o.Product.User.shopLogo,
                        shopeDescription = o.Product.User.shopeDescription,
                        adress = o.Product.User.adress,
                        localAdress = o.Product.User.localAdress,
                        hasLocal = o.Product.User.hasLocal,
                        field = o.Product.User.field,
                        personalSite = o.Product.User.personalSite,
                        facebook = o.Product.User.facebook,
                        instagram = o.Product.User.instagram,
                        rating = o.Product.User.rating,
                        mySource = o.Product.User.mySource,
                        joinedAt = o.Product.User.joinedAt,
                        username = o.Product.User.User.username,
                        email = o.Product.User.User.email,
                        photo = o.Product.User.User.photo
                    }
                }
            };
        }

        public static List<OrderDTO> ToDTOList(List<Order> orders)
        {
            return orders.Select(o => ToDTO(o)).ToList();
        }
    }
}
