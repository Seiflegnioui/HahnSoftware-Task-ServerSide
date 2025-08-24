using hahn.Application.buyer.Queries;
using hahn.Application.DTOs;
using hahn.Application.seller.Queries;
using hahn.Application.Validators;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.buyer.Handlers
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
            var buyer =await _BuyerRepository.GetBuyerByIdAsync(request.UserId, cancellationToken);
            if (buyer == null)
            {
                return CustomResult<BuyerDTO>.Fail(new List<string> { $"Buyer {request.UserId} not found" });
            }

            var buyerDto = new BuyerDTO
            {
                userId = buyer.userId,
                bio = buyer.bio,
                brthdate = buyer.brthdate,
                id = buyer.id,
                adress = buyer.adress,
                mySource = buyer.mySource,
                joinedAt = buyer.joinedAt
            };
            return CustomResult<BuyerDTO>.Ok(buyerDto);
        }
    }

}
