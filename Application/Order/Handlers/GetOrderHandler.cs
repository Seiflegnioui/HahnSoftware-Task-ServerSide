using hahn.Application.DTOs;
using hahn.Application.Order.Queries;
using hahn.Application.Validators;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.Order.Handlers
{
    public class GetOrderHandler(IOrderRepository repository) : IRequestHandler<GetOrderQuery, CustomResult<OrderDTO>>
    {
        public async Task<CustomResult<OrderDTO>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var dto = await repository.GetOrderAsync(request, cancellationToken);
            return dto;
        }
    }
}
