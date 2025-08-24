using hahn.Application.buyer.Commands;
using hahn.Application.DTOs;
using hahn.Application.order.Commands;
using hahn.Application.order.Queries;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Enums;
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

        public async Task<Order> AddOrderAsync(Order order, CancellationToken cancellationToken)
        {
            await context.Orders.AddAsync(order, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return order;
        }

        public async Task<List<Order>> GetOrderAsync(CancellationToken cancellationToken, int? sellerId = null, int? buyerId = null)
        {
            var query = context.Orders
                .Include(o => o.Product).
                ThenInclude(p => p.User) // SELLER
                .ThenInclude(u => u.User)
                .Include(o => o.Buyer).ThenInclude(b => b.User)
                .AsNoTracking();

            if (buyerId != null)
                query = query.Where(o => o.buyerId == buyerId);

            if (sellerId != null)
                query = query.Where(o => o.Product.sellerId == sellerId);


            return await query.ToListAsync();
        }
        public async Task<Order?> GetOrderByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Orders
                .Include(o => o.Product)
                    .ThenInclude(p => p.User)
                        .ThenInclude(u => u.User)
                .Include(o => o.Buyer)
                    .ThenInclude(b => b.User)
                .FirstOrDefaultAsync(o => o.id == id, cancellationToken);
        }

        public async Task<Order> UpdateOrderState(Order order, CancellationToken cancellationToken, OrderState state)
        {
            order.ChangeState(state);
            context.Orders.Update(order); 
            await context.SaveChangesAsync(cancellationToken);
            return order;
        }

    }

}