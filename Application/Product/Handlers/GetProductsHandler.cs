using System.Security.Claims;
using hahn.Application.DTOs;
using hahn.Application.product.Commands;
using hahn.Application.product.Queries;
using hahn.Application.Validators;
using hahn.Domain.Repositories;
using hahn.Infrastructure.Services;
using MediatR;
using System.Linq;

namespace hahn.Application.product.Handlers
{
    public class GetProductsHandler(IProductRepository repository) : IRequestHandler<GetProductsQuery, CustomResult<ProductDTO>>

    {

        public async Task<CustomResult<ProductDTO>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            if (request.id.HasValue)
            {
                var product = await repository.GetProductByIdAsync(cancellationToken,request.id);
                if (product == null)
                    return CustomResult<ProductDTO>.Fail(new List<string> { $"Product {request.id} not found" });

                return CustomResult<ProductDTO>.Ok(ProductMapper.ToDTO(product));
            }
            else if (request.sellerId.HasValue) 
            {
                var filtredProducts = await repository.FilterProductsAsync(cancellationToken,request.sellerId);
                return CustomResult<ProductDTO>.Ok(ProductMapper.ToDTOList(filtredProducts));
            }
            var products =await repository.FilterProductsAsync(cancellationToken);
            var dto = ProductMapper.ToDTOList(products);
            return CustomResult<ProductDTO>.Ok(dto);
        }

    }
}