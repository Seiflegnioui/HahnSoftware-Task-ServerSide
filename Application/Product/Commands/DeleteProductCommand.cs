using hahn.Application.DTOs;
using hahn.Application.Validators;
using MediatR;

namespace hahn.Application.product.Commands
{
    public class DeleteProductCommand : IRequest<CustomResult<ProductDTO>>
    {
        public int id { get; set; }
    }
}