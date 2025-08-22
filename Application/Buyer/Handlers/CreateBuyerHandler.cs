using System.Security.Claims;
using hahn.Application.Buyer.Commands;
using hahn.Application.DTOs;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using hahn.Domain.ValueObject;
using MediatR;

namespace hahn.Application.Buyer.Handlers
{
    public class CreateBuyerHandler(IHttpContextAccessor http,IBuyerRepository repository) : IRequestHandler<CreateBuyerCommand, CustomResult<BuyerDTO>>
    {
        public async Task<CustomResult<BuyerDTO>> Handle(CreateBuyerCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("User not authenticated.");

            var userId = int.Parse(userIdClaim);

            var result = await repository.AddBuyerAsync(request,cancellationToken,userId);
            
            return result;
        }
    }
}