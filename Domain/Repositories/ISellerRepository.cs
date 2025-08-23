
using hahn.Application.DTOs;
using hahn.Application.Seller.Commands;
using hahn.Application.Seller.Queries;
using hahn.Application.Users.Commands;
using hahn.Application.Validators;
using hahn.Domain.Entities;

namespace hahn.Domain.Repositories
{
    public interface ISellerRepository
    {
        Task<CustomResult<SellerDTO>> AddSellerAsync(CreateSellerCommand request, CancellationToken cancellationToken,int userId);
        Task<CustomResult<SellerDTO>> GetSellerByIdAsync(GetSellerByIdQuery request, CancellationToken cancellationToken);

    }
}