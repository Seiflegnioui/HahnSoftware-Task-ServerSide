using System.Text;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using hahn.Infrastructure.Presistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hahn.Infrastructure.Repositories
{
    public class SellerRepository(AppDbContext context) : ISellerRepository
    {
 
        public async Task<Seller> AddSellerAsync(Seller seller, CancellationToken cancellationToken)
        {
            await context.Sellers.AddAsync(seller, cancellationToken);
            var user = await context.Users.FirstOrDefaultAsync(u => u.id == seller.userId, cancellationToken);
            user.AuthCompleted = true;
            seller.User = user;
            await context.SaveChangesAsync();
            return seller;
        }
        public async Task<Seller?> GetSellerByIdAsync(int userId, CancellationToken cancellationToken)
        {
            var seller = await context.Sellers
                .FirstOrDefaultAsync(s => s.userId == userId, cancellationToken);
            return seller;

            
        }

      
    }
}