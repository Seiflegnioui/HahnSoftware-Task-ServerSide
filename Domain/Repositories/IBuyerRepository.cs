
using hahn.Application.Buyer.Commands;
using hahn.Application.Buyer.Queries;
using hahn.Application.DTOs;
using hahn.Application.Seller.Commands;
using hahn.Application.Users.Commands;
using hahn.Application.Validators;
using hahn.Domain.Entities;

namespace hahn.Domain.Repositories
{
    public interface IBuyerRepository
    {
        Task<CustomResult<BuyerDTO>> AddBuyerAsync(CreateBuyerCommand request, CancellationToken cancellationToken, int userId);
        Task<CustomResult<BuyerDTO>> GetBuyerByIdAsync(GetBuyerByIdQuery request, CancellationToken cancellationToken);

    }
}