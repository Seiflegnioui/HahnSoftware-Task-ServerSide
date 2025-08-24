using hahn.Application.DTOs;
using hahn.Application.Validators;
using hahn.Domain.Enums;
using MediatR;

namespace hahn.Application.order.Commands
{
    public class UpdateOrderStateCommand : IRequest<CustomResult<OrderDTO>>
    {
        public int orderId { get; set; } 
        public OrderState state { get; set; }
    }
}