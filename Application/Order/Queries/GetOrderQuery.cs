using hahn.Application.DTOs;
using hahn.Application.Validators;
using MediatR;

namespace hahn.Application.order.Queries
{
    public class GetOrderQuery : IRequest<CustomResult<OrderDTO>>
    {
        public int? sellerId { get; set; }
        public int? buyerId { get; set; }
    }
}