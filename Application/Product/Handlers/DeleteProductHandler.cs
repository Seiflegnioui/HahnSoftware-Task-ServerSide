using hahn.Application.DTOs;
using hahn.Application.product.Commands;
using hahn.Application.Validators;
using hahn.Domain.Repositories;
using hahn.Infrastructure.Services;
using MediatR;

namespace hahn.Application.product.Handlers
{
    public class DeleteProductHandler(IProductRepository repository) : IRequestHandler<DeleteProductCommand, CustomResult<ProductDTO>>
    {

        public async Task<CustomResult<ProductDTO>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await repository.DeleteUserAsync(request.id,cancellationToken);
            if (product is null)
            {
            return CustomResult<ProductDTO>.Fail(["no product to delete"]);
                
            }
            var productDto = new ProductDTO()
            {
                id = product.id
            };
            return CustomResult<ProductDTO>.Ok(productDto);

        }
    }
}