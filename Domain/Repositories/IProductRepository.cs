using hahn.Application.DTOs;
using hahn.Application.product.Commands;
using hahn.Application.product.Queries;
using hahn.Application.Validators;
using hahn.Domain.Entities;

namespace hahn.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddProductrAsync(Product product, CancellationToken cancellationToken);
        Task<Product> DeleteUserAsync(int id, CancellationToken cancellationToken);
        Task<Product> GetProductByIdAsync( CancellationToken cancellationToken,int? id);
        Task<List<Product>> FilterProductsAsync( CancellationToken cancellationToken,int? sellerId = null);
    }
}