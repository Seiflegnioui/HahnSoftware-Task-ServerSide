using hahn.Application.DTOs;
using hahn.Application.Validators;
using MediatR;

namespace hahn.Application.product.Queries
{
    public class GetProductsQuery : IRequest<CustomResult<ProductDTO>>
    {
        public int? id { get; set; }
        public int? sellerId { get; set; }
    }
}