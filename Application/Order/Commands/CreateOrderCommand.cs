using hahn.Application.DTOs;
using hahn.Application.Validators;
using MediatR;

namespace hahn.Application.Order.Commands
{
    public class CreateOrderCommand : IRequest<CustomResult<OrderDTO>>
    {

        public int buyerId { get; set; } 
        public int productId { get; set; } 
        public int quantity { get; set; }
    }
}