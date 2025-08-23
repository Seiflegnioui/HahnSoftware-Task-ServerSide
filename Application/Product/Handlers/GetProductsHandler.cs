using System.Security.Claims;
using hahn.Application.DTOs;
using hahn.Application.Product.Commands;
using hahn.Application.Product.Queries;
using hahn.Application.Validators;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.Product.Handlers
{
    public class GetProductsHandler(IProductRepository repository, IHttpContextAccessor http): IRequestHandler<GetProductsQuery, CustomResult<ProductDTO>>

    {
        public async Task<CustomResult<ProductDTO>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await repository.GetProductsAsync(request, cancellationToken);
            return products;
        }
    }
}