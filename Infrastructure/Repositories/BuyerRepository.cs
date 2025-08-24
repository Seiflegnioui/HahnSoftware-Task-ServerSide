using hahn.Application.buyer.Commands;
using hahn.Application.buyer.Queries;
using hahn.Application.DTOs;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using hahn.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace hahn.Infrastructure.Repositories
{
    public class BuyerRepository(AppDbContext context) : IBuyerRepository
    {

        public async Task<Buyer> AddBuyerAsync(Buyer buyer, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.id == buyer.userId);
            await context.Buyers.AddAsync(buyer, cancellationToken);
            user!.AuthCompleted = true;
            await context.SaveChangesAsync(cancellationToken);
            return buyer;
        }

        public async Task<Buyer> GetBuyerByIdAsync(int userId, CancellationToken cancellationToken)
        {
             var buyer = await context.Buyers
                .FirstOrDefaultAsync(s => s.userId == userId, cancellationToken);
            return buyer ;
        }
    }
}