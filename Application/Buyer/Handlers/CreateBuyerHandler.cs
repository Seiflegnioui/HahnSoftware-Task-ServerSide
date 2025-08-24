using System.Security.Claims;
using hahn.Application.buyer.Commands;
using hahn.Application.DTOs;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using hahn.Domain.ValueObject;
using MediatR;

namespace hahn.Application.buyer.Handlers
{
    public class CreateBuyerHandler(IHttpContextAccessor http,IBuyerRepository repository) : IRequestHandler<CreateBuyerCommand, CustomResult<BuyerDTO>>
    {
        public async Task<CustomResult<BuyerDTO>> Handle(CreateBuyerCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("User not authenticated.");

            var userId = int.Parse(userIdClaim);

            var buyer = Buyer.Create(
                userId : userId,
                adress : request.adress,
                bio : request.bio,
                brthdate : request.brthdate,
                mySource : request.mySource
            );

            buyer = await repository.AddBuyerAsync(buyer,cancellationToken);
              var buyerDTO = new BuyerDTO()
            {
                id = buyer.id,
                userId = buyer.userId,
                adress = buyer.adress,
                bio = buyer.bio!,
                brthdate = buyer.brthdate,
                mySource = buyer.mySource,
                joinedAt = buyer.joinedAt,
            };
            
            return CustomResult<BuyerDTO>.Ok(buyerDTO);
        }
    }
}