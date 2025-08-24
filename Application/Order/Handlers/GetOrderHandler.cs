using hahn.Application.DTOs;
using hahn.Application.order.Queries;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using hahn.Infrastructure.Services;
using MediatR;

namespace hahn.Application.order.Handlers
{
    public class GetOrderHandler(IOrderRepository repository) : IRequestHandler<GetOrderQuery, CustomResult<OrderDTO>>
    {
        public async Task<CustomResult<OrderDTO>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            List<Order> orders;
            if (request.buyerId.HasValue)
            {
                orders = await repository.GetOrderAsync(cancellationToken, request.buyerId);
            }
        
            orders = await repository.GetOrderAsync(cancellationToken, request.sellerId);

            var dto = OrderMapper.ToDTOList(orders);

            return CustomResult<OrderDTO>.Ok(dto);
        }
    }
}
