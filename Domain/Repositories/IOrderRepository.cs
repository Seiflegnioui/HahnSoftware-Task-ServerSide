using hahn.Application.DTOs;
using hahn.Application.order.Commands;
using hahn.Application.order.Queries;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Enums;

namespace hahn.Domain.Repositories
{
public interface IOrderRepository
{
    Task<Order> AddOrderAsync(Order order, CancellationToken cancellationToken);
    Task<List<Order>> GetOrderAsync(CancellationToken cancellationToken, int? sellerId = null, int? buyerId = null);
        Task<Order> UpdateOrderState(Order order,CancellationToken cancellationToken, OrderState state);
        Task<Order> GetOrderByIdAsync( int id,CancellationToken cancellationToken);
}

}