using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using hahn.Application.Services;
using hahn.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace hahn.Infrastructure.Services
{
    public class CreateJwtService(IConfiguration config) : ICreateJwtService
    {
        public string CreateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Role, user.role.ToString())
            };

            var bytekey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("jwt_infos:key")!));
            var creds = new SigningCredentials(bytekey, SecurityAlgorithms.HmacSha512);

            var descriptor = new JwtSecurityToken(
                issuer: config.GetValue<string>("jwt_infos:issuer"),
                audience: config.GetValue<string>("jwt_infos:audience"),
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddDays(2),
                claims: claims

            );
            return new JwtSecurityTokenHandler().WriteToken(descriptor);
        }
    }
}