using hahn.Application.DTOs;
using hahn.Application.Product.Commands;
using hahn.Application.Product.Queries;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using hahn.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;


namespace hahn.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        public async Task<CustomResult<ProductDTO>> AddProductrAsync(CreateProductCommand request, CancellationToken cancellationToken, int userId)
        {
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
            var seller = await context.Sellers.FirstOrDefaultAsync(u => u.userId == userId);
            var product = new Product()
            {
                sellerId = seller!.id,
                category = request.category,
                description = request.description,
                name = request.name,
                price = request.price,
                quantity = request.quantity,
                image = filename
            };
            await context.Products.AddAsync(product, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

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

        public async Task<CustomResult<ProductDTO>> DeleteUserAsync(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.id == request.id, cancellationToken);

            if (product is null)
            {
                return CustomResult<ProductDTO>.Fail(new List<string>() { "Product not found" });
            }

            var dto = await ProductDTOMapperAsync(context.Products.Where(p => p.id == request.id), cancellationToken);
            context.Products.Remove(product);
            await context.SaveChangesAsync(cancellationToken);

            return CustomResult<ProductDTO>.Ok(dto!);
        }


        public async Task<CustomResult<ProductDTO>> GetProductsAsync(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var query = context.Products
                .Include(p => p.User) // Seller
                .ThenInclude(s => s.User) // Seller's User
                .AsNoTracking();

            if (request.sellerId != null)
            {
                query = query.Where(p => p.sellerId == request.sellerId);
            }

            if (request.id != null)
            {
                var product = await query.FirstOrDefaultAsync(p => p.id == request.id, cancellationToken);
                if (product == null)
                    return CustomResult<ProductDTO>.Fail(new List<string> { $"Product {request.id} not found" });

                var productDto = await ProductDTOMapperAsync(context.Products.Where(p=>p.id == product.id), cancellationToken);
                return CustomResult<ProductDTO>.Ok(productDto!); 
            }

            var products = await ProductDtoListMapperAsync(query, cancellationToken);
            return CustomResult<ProductDTO>.Ok(products); 
        }


        private async Task<List<ProductDTO>> ProductDtoListMapperAsync(
        IQueryable<Product> productsQuery,
        CancellationToken cancellationToken)
        {
            var products = await productsQuery
                .Include(p => p.User)
                .ThenInclude(u => u.User)
                .AsNoTracking()
                .Select(p => new ProductDTO
                {
                    id = p.id,
                    sellerId = p.sellerId,
                    name = p.name,
                    description = p.description,
                    category = p.category,
                    image = p.image,
                    price = p.price,
                    quantity = p.quantity,
                    reviews = p.reviews,
                    addedAt = p.addedAt,
                    Seller = new SellerDTO
                    {
                        id = p.User.id,
                        userId = p.User.userId,
                        shopName = p.User.shopName,
                        shopLogo = p.User.shopLogo,
                        shopeDescription = p.User.shopeDescription,
                        field = p.User.field,
                        adress = p.User.adress,
                        localAdress = p.User.localAdress,
                        hasLocal = p.User.hasLocal,
                        personalSite = p.User.personalSite,
                        facebook = p.User.facebook,
                        instagram = p.User.instagram,
                        rating = p.User.rating,
                        mySource = p.User.mySource,
                        joinedAt = p.User.joinedAt,

                        email = p.User.User.email,
                        username = p.User.User.username,
                        photo = p.User.User.photo
                    }
                })
                .ToListAsync(cancellationToken);

            return products;
        }

        private async Task<ProductDTO?> ProductDTOMapperAsync(
            IQueryable<Product> productQuery,
            CancellationToken cancellationToken)
        {
            return await productQuery
                .Include(p => p.User)
                .ThenInclude(u => u.User)
                .AsNoTracking()
                .Select(p => new ProductDTO
                {
                    id = p.id,
                    sellerId = p.sellerId,
                    name = p.name,
                    description = p.description,
                    category = p.category,
                    image = p.image,
                    price = p.price,
                    quantity = p.quantity,
                    reviews = p.reviews,
                    addedAt = p.addedAt,
                    Seller = new SellerDTO
                    {
                        id = p.User.id,
                        userId = p.User.userId,
                        shopName = p.User.shopName,
                        shopLogo = p.User.shopLogo,
                        shopeDescription = p.User.shopeDescription,
                        field = p.User.field,
                        adress = p.User.adress,
                        localAdress = p.User.localAdress,
                        hasLocal = p.User.hasLocal,
                        personalSite = p.User.personalSite,
                        facebook = p.User.facebook,
                        instagram = p.User.instagram,
                        rating = p.User.rating,
                        mySource = p.User.mySource,
                        joinedAt = p.User.joinedAt,

                        email = p.User.User.email,
                        username = p.User.User.username,
                        photo = p.User.User.photo
                    }
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

    }
}