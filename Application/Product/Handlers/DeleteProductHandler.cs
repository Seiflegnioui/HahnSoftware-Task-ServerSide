using hahn.Application.DTOs;
using hahn.Application.Product.Commands;
using hahn.Application.Validators;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.Product.Handlers
{
    public class DeleteProductHandler(IProductRepository repository) : IRequestHandler<DeleteProductCommand, CustomResult<ProductDTO>>
    {
        public int? sellerId { get; set; }

        public async Task<CustomResult<ProductDTO>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await repository.DeleteUserAsync(request,cancellationToken);
            return product;

            throw new NotImplementedException();
        }
    }
}