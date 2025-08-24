using hahn.Application.DTOs;
using hahn.Application.order.Commands;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Enums;
using hahn.Domain.Repositories;
using hahn.Infrastructure.Services;
using MediatR;

namespace hahn.Application.order.Handlers
{
    public class UpdateOrderStateHandler : IRequestHandler<UpdateOrderStateCommand, CustomResult<OrderDTO>>
{
    private readonly IOrderRepository _repository;

    public UpdateOrderStateHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomResult<OrderDTO>> Handle(UpdateOrderStateCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetOrderByIdAsync(request.orderId, cancellationToken);
        if (order is null)
            return CustomResult<OrderDTO>.Fail(new List<string> { "Order not found." });

        try
        {
            await _repository.UpdateOrderState(order, cancellationToken, request.state);

            var dto = OrderMapper.ToDTO(order);
            return CustomResult<OrderDTO>.Ok(dto);
        }
        catch (Exception ex)
        {
            return CustomResult<OrderDTO>.Fail(new List<string> { ex.Message });
        }
    }
}

}
