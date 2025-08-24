using System.Security.Claims;
using hahn.Application.DTOs;
using hahn.Application.product.Commands;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.product.Handlers
{
    public class CreateProductHandler(IProductRepository repository, IHttpContextAccessor http, ISellerRepository sellerRepository): IRequestHandler<CreateProductCommand, CustomResult<ProductDTO>>

    {
        public async Task<CustomResult<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("User not authenticated.");

            var userId = int.Parse(userIdClaim);

              var folder_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "products");
            if (!Directory.Exists(folder_path))
            {
                Directory.CreateDirectory(folder_path);
            }

            string filename = string.Empty;

            if (request.image != null && request.image.Length > 0)
            {

                filename = userId + Guid.NewGuid().ToString() + Path.GetExtension(request.image.FileName);
                var filepath = Path.Combine(folder_path, filename);

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await request.image.CopyToAsync(stream);
                }
            }
            else
            {
                filename = "default_image.jpg";
            }
            var seller = await sellerRepository.GetSellerByIdAsync(userId, cancellationToken);
            var product = Product.Create(
                sellerId : seller.id,
                category: request.category,
                description: request.description,
                name: request.name,
                price: request.price,
                quantity: request.quantity,
                image: filename
            );

            if (seller == null)
                throw new InvalidOperationException($"This user {userId} is not a seller yet.");


            product = await repository.AddProductrAsync(product,cancellationToken);

            var productDto = new ProductDTO()
            {
                id = product.id,
                sellerId = product.sellerId,
                description = product.description,
                name = product.name,
                price = product.price,
                image = product.image,
                quantity = product.quantity,
                category = product.category,
                addedAt = product.addedAt,
                reviews = product.reviews,

            };
            return CustomResult<ProductDTO>.Ok(productDto);
        }
    }
}