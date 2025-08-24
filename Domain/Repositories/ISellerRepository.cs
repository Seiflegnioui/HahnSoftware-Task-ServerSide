
using hahn.Application.DTOs;
using hahn.Application.seller.Commands;
using hahn.Application.seller.Queries;
using hahn.Application.Users.Commands;
using hahn.Application.Validators;
using hahn.Domain.Entities;

namespace hahn.Domain.Repositories
{
    public interface ISellerRepository
    {
        Task<Seller>GetSellerByIdAsync (int userId, CancellationToken cancellationToken);
        Task<Seller>AddSellerAsync (Seller seller,CancellationToken cancellationToken);

    }
}