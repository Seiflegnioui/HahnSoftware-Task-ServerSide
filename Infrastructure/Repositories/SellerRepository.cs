using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using hahn.Application.DTOs;
using hahn.Application.Seller.Commands;
using hahn.Application.Seller.Queries;
using hahn.Application.Users.Commands;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using hahn.Infrastructure.Presistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace hahn.Infrastructure.Repositories
{
    public class SellerRepository(AppDbContext context, ILogger<SellerRepository> log) : ISellerRepository
    {
        public async Task<CustomResult<SellerDTO>> AddSellerAsync(CreateSellerCommand request, CancellationToken cancellationToken,int userId)
        {
           
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

            var user = await context.Users.FirstOrDefaultAsync(u => u.id == userId, cancellationToken);
            user.AuthCompleted = true;
            context.Users.Update(user);

            var seller = new Seller()
            {
                userId = userId,
                User = user,
                adress = request.adress,
                field = request.field,
                hasLocal = request.hasLocal,
                localAdress = request.localAdress,
                instagram = request.instagram,
                facebook = request.facebook,
                personalSite = request.personalSite,
                mySource = request.mySource,
                shopName = request.shopName,
                shopLogo = filename,
                shopeDescription = request.shopDescription,
            };

            await context.Sellers.AddAsync(seller, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

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

        public async Task<CustomResult<SellerDTO>> GetSellerByIdAsync(GetSellerByIdQuery request, CancellationToken cancellationToken)
        {
            var seller = await context.Sellers
                .FirstOrDefaultAsync(s => s.userId == request.UserId, cancellationToken);

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