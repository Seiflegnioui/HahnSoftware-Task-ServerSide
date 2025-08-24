using System.Security.Claims;
using hahn.Application.DTOs;
using hahn.Application.seller.Commands;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.seller.Handlers
{
    public class CreateSellerHandler(IHttpContextAccessor http, ISellerRepository repository) : IRequestHandler<CreateSellerCommand, CustomResult<SellerDTO>>
    {

        public async Task<CustomResult<SellerDTO>> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("User not authenticated.");

            var userId = int.Parse(userIdClaim);

            var folder_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "shoplogos");
            if (!Directory.Exists(folder_path))
                Directory.CreateDirectory(folder_path);

            string filename;

            if (request.shopLogo != null && request.shopLogo.Length > 0)
            {
                filename = userId + "-" + Guid.NewGuid().ToString() + Path.GetExtension(request.shopLogo.FileName);
                var filepath = Path.Combine(folder_path, filename);

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await request.shopLogo.CopyToAsync(stream);
                }
            }
            else
            {
                filename = "default.png";
            }

            var seller = Seller.Create(
                userId: userId,
                adress: request.adress,
                field: request.field,
                hasLocal: request.hasLocal,
                localAdress: request.localAdress,
                instagram: request.instagram,
                facebook: request.facebook,
                personalSite: request.personalSite,
                mySource: request.mySource,
                shopName: request.shopName,
                shopLogo: filename,
                shopDescription: request.shopDescription
            );

            seller = await repository.AddSellerAsync(seller, cancellationToken);

            var sellerDTO = new SellerDTO()
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
                joinedAt = seller.joinedAt,

                email = seller.User.email,
                username = seller.User.username,
                photo = seller.User.photo,
            };

            return CustomResult<SellerDTO>.Ok(sellerDTO);
        }
    }
}
