using System.Security.Claims;
using hahn.Application.DTOs;
using hahn.Application.Order.Commands;
using hahn.Application.Seller.Commands;
using hahn.Application.Validators;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.Order.Handlers
{
    public class CreateOrderHandler( IOrderRepository repository) : IRequestHandler<CreateOrderCommand, CustomResult<OrderDTO>>
    {
        public async Task<CustomResult<OrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await repository.AddOrderAsync(request, cancellationToken);
            return order;
        }
    }
}
