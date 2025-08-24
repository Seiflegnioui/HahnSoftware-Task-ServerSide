using hahn.Application.Validators;
using hahn.Domain.Entities;

namespace hahn.Domain.Repositories
{
    public interface IBuyerRepository
    {
        Task<Buyer> AddBuyerAsync(Buyer buyer, CancellationToken cancellationToken);
        Task<Buyer> GetBuyerByIdAsync(int userId, CancellationToken cancellationToken);
    }
}