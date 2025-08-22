using hahn.Application.Buyer.Commands;
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

        public async Task<CustomResult<BuyerDTO>> AddBuyerAsync(CreateBuyerCommand request, CancellationToken cancellationToken, int userId)
        {
            var buyer = new Buyer()
            {
                userId = userId,
                adress = request.adress,
                bio = request.bio,
                brthdate = request.brthdate,
                mySource = request.mySource,

            };
            var user = await context.Users.FirstOrDefaultAsync(u => u.id == userId);
            
            user!.AuthCompleted = true;
            await context.Buyers.AddAsync(buyer, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);


            var buyerDTO = new BuyerDTO()
            {
                id = buyer.id,
                userId = buyer.userId,
                adress = buyer.adress,
                bio = buyer.bio,
                brthdate = buyer.brthdate,
                mySource = buyer.mySource,
                joinedAt = buyer.joinedAt,
            };

            return CustomResult<BuyerDTO>.Ok(buyerDTO);
        }

    }
}