using hahn.Application.DTOs;
using hahn.Application.Validators;
using MediatR;

namespace hahn.Application.Buyer.Queries
{
    public class GetBuyerByIdQuery : IRequest<CustomResult<BuyerDTO>>
    {
        public int UserId { get; set; }

        public GetBuyerByIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
