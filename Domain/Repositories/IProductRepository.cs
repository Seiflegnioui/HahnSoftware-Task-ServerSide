using hahn.Application.Buyer.Commands;
using hahn.Application.DTOs;
using hahn.Application.Product.Commands;
using hahn.Application.Product.Queries;
using hahn.Application.Seller.Commands;
using hahn.Application.Users.Commands;
using hahn.Application.Validators;
using hahn.Domain.Entities;

namespace hahn.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<CustomResult<ProductDTO>> AddProductrAsync(CreateProductCommand request, CancellationToken cancellationToken,int userId);
        Task<CustomResult<ProductDTO>> DeleteUserAsync(DeleteProductCommand request, CancellationToken cancellationToken);
        Task<CustomResult<ProductDTO>> GetProductsAsync(GetProductsQuery request, CancellationToken cancellationToken);
    }
}