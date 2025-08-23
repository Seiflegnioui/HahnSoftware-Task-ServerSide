using hahn.Application.Buyer.Commands;
using hahn.Application.DTOs;
using hahn.Application.Order.Commands;
using hahn.Application.Order.Queries;
using hahn.Application.Product.Commands;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using hahn.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace hahn.Infrastructure.Repositories
{

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext context;

    public OrderRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<CustomResult<OrderDTO>> AddOrderAsync(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order()
        {
            buyerId = request.buyerId,
            productId = request.productId,
            quantity = request.quantity
        };

        await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var dto = new OrderDTO()
        {
            id = order.id,
            quantity = order.quantity,
            addedAt = order.addedAt
        };

        return CustomResult<OrderDTO>.Ok(dto);
    }

    public async Task<CustomResult<OrderDTO>> GetOrderAsync(GetOrderQuery request, CancellationToken cancellationToken)
{
    var query = context.Orders
        .Include(o => o.Product).
        ThenInclude(p => p.User) // SELLER
        .ThenInclude(u => u.User) 
        .Include(o => o.Buyer).ThenInclude(b=> b.User)
        .AsNoTracking();

    if (request.buyerId != null)
        query = query.Where(o => o.buyerId == request.buyerId);

    if (request.sellerId != null)
        query = query.Where(o => o.Product.sellerId == request.sellerId);

    var orders = await query.Select(o => new OrderDTO()
    {
        id = o.id,
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
        },
        quantity = o.quantity,
        state = o.state,
        addedAt = o.addedAt
    }).ToListAsync(cancellationToken);

    return CustomResult<OrderDTO>.Ok(orders);
}
}

}