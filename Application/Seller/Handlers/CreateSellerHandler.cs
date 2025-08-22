using System.Security.Claims;
using hahn.Application.DTOs;
using hahn.Application.Seller.Commands;
using hahn.Application.Validators;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.Seller.Handlers
{
    public class CreateSellerHandler(IHttpContextAccessor http,ISellerRepository repository) : IRequestHandler<CreateSellerCommand, CustomResult<SellerDTO>>
    {

        public async Task<CustomResult<SellerDTO>> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("User not authenticated.");

            var userId = int.Parse(userIdClaim);

            var result = await repository.AddSellerAsync(request,cancellationToken,userId);
            return result;
        }
    }
}
