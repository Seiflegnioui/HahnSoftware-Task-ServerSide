using hahn.Application.DTOs;
using hahn.Application.Validators;
using MediatR;

namespace hahn.Application.seller.Queries
{
    public class GetSellerByIdQuery : IRequest<CustomResult<SellerDTO>>
    {
        public int UserId { get; set; }

        public GetSellerByIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
