using hahn.Application.DTOs;
using hahn.Application.Seller.Queries;
using hahn.Application.Validators;
using hahn.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace hahn.Application.Seller.Handlers
{
    

    public class GetSellerByIdHandler : IRequestHandler<GetSellerByIdQuery, CustomResult<SellerDTO>>
    {
        private readonly ISellerRepository _sellerRepository;

        public GetSellerByIdHandler(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
        }

        public async Task<CustomResult<SellerDTO>> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _sellerRepository.GetSellerByIdAsync(request.UserId, cancellationToken);
        }
    }

}
