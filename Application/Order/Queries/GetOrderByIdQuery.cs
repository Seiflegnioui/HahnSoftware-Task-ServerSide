using hahn.Application.DTOs;
using hahn.Application.Validators;
using MediatR;

namespace hahn.Application.order.Queries
{
    public class GetOrderByIdQuery : IRequest<CustomResult<OrderDTO>>
    {
        public int id { get; set; }
    }
}