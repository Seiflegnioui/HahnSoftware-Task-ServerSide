using System.Security.Claims;
using hahn.Application.DTOs;
using hahn.Application.Product.Commands;
using hahn.Application.Validators;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.Product.Handlers
{
    public class CreateProductHandler(IProductRepository repository, IHttpContextAccessor http): IRequestHandler<CreateProductCommand, CustomResult<ProductDTO>>

    {
        public async Task<CustomResult<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("User not authenticated.");

            var userId = int.Parse(userIdClaim);

            var result = await repository.AddProductrAsync(request,cancellationToken,userId);
            return result;
        }
    }
}