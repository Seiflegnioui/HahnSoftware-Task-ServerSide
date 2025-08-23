using hahn.Application.Buyer.Queries;
using hahn.Application.DTOs;
using hahn.Application.Seller.Queries;
using hahn.Application.Validators;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.Buyer.Handlers
{
    

    public class GetBuyerByIdHandler : IRequestHandler<GetBuyerByIdQuery, CustomResult<BuyerDTO>>
    {
        private readonly IBuyerRepository _BuyerRepository;

        public GetBuyerByIdHandler(IBuyerRepository BuyerRepository)
        {
            _BuyerRepository = BuyerRepository;
        }

        public async Task<CustomResult<BuyerDTO>> Handle(GetBuyerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _BuyerRepository.GetBuyerByIdAsync(request, cancellationToken);
        }
    }

}
