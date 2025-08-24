using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using hahn.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;


namespace hahn.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        public async Task<Product> AddProductrAsync(Product product, CancellationToken cancellationToken)
        {
            await context.Products.AddAsync(product, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return product;
        }
        public async Task<Product?> DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.id == id, cancellationToken);
            if (product is not null)
            {
                var productCopy = product.Copy();  // copy before deletion
                context.Products.Remove(product);
                await context.SaveChangesAsync(cancellationToken);
                return productCopy;
            }
            return null;
        }


        public async Task<Product?> GetProductByIdAsync(CancellationToken cancellationToken = default, int? id = null)
        {
            var product = await context.Products
                .Include(p => p.User) // Seller
                .ThenInclude(s => s.User) // Seller’s User
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.id == id, cancellationToken);

            return product;
        }


        public async Task<List<Product>> FilterProductsAsync(CancellationToken cancellationToken = default, int? sellerId = null)
        {
            var query = context.Products
                .Include(p => p.User) // Seller
                .ThenInclude(s => s.User) // Seller's User
                .AsNoTracking();

            if (sellerId.HasValue)
                query = query.Where(p => p.sellerId == sellerId.Value);

            return await query.ToListAsync(cancellationToken);
        }

    }
}