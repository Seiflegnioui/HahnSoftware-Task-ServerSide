using hahn.Application.DTOs;
using hahn.Application.order.Commands;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.order.Handlers
{
    public class CreateOrderHandler( IOrderRepository repository) : IRequestHandler<CreateOrderCommand, CustomResult<OrderDTO>>
    {
        public async Task<CustomResult<OrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = Order.Send(
            buyerId: request.buyerId,
            productId: request.productId,
            quantity: request.quantity
            );
        {
        };

            order = await repository.AddOrderAsync(order, cancellationToken);

                var dto = new OrderDTO()
        {
            id = order.id,
            quantity = order.quantity,
            addedAt = order.addedAt
        };

            return CustomResult<OrderDTO>.Ok(dto);
        }
    }
}
