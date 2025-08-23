using hahn.Application.Buyer.Commands;
using hahn.Application.DTOs;
using hahn.Application.Order.Commands;
using hahn.Application.Order.Queries;
using hahn.Application.Validators;

namespace hahn.Domain.Repositories
{
public interface IOrderRepository
{
    Task<CustomResult<OrderDTO>> AddOrderAsync(CreateOrderCommand request, CancellationToken cancellationToken);
    Task<CustomResult<OrderDTO>> GetOrderAsync(GetOrderQuery request, CancellationToken cancellationToken);
}

}