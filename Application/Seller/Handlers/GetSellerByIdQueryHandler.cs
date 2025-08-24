using hahn.Application.DTOs;
using hahn.Application.seller.Queries;
using hahn.Application.Validators;
using hahn.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace hahn.Application.seller.Handlers
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
            var seller = await _sellerRepository.GetSellerByIdAsync(request.UserId, cancellationToken);

            if (seller == null)
            {
                return CustomResult<SellerDTO>.Fail(new List<string> { $"Seller {request.UserId} not found" });
            }

            var sellerDto = new SellerDTO
            {
                id = seller.id,
                userId = seller.userId,
                shopName = seller.shopName,
                shopLogo = seller.shopLogo,
                shopeDescription = seller.shopeDescription,
                adress = seller.adress,
                hasLocal = seller.hasLocal,
                localAdress = seller.localAdress,
                field = seller.field,
                personalSite = seller.personalSite,
                facebook = seller.facebook,
                instagram = seller.instagram,
                rating = seller.rating,
                mySource = seller.mySource,
                joinedAt = seller.joinedAt
            };

            return CustomResult<SellerDTO>.Ok(sellerDto);
        }
    }

}
