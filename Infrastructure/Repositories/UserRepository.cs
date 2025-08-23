using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using hahn.Application.DTOs;
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
    public class UserRepository(AppDbContext context, IConfiguration config) : IUserRepository
    {
        public async Task<UserAuthResult<UserDTO>> AddAsync(CreateUserCommand request)
        {

            var folder_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "users");
            if (!Directory.Exists(folder_path))
                Directory.CreateDirectory(folder_path);

            string filename;

            if (request.photo != null && request.photo.Length > 0)
            {
                filename = request.username + "-" + Guid.NewGuid().ToString() + Path.GetExtension(request.photo.FileName);
                var filepath = Path.Combine(folder_path, filename);

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await request.photo.CopyToAsync(stream);
                }
            }
            else
            {
                filename = "default.png";
            }

            var user = new User();

            var hashed = new PasswordHasher<User>().HashPassword(user, request.password);

            user.email = request.email;
            user.username = request.username;
            user.role = request.role;
            user.phone = request.phone;
            user.photo = filename;
            user.hashedPpassword = hashed;

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var userDto = new UserDTO
            {
                id = user.id,
                email = user.email,
                username = user.username,
                role = user.role,
                phone = user.phone,
                photo = user.photo,
                AuthCompleted = user.AuthCompleted,
            };

            var token = CreateToken(userDto);
            return UserAuthResult<UserDTO>.Ok(userDto, token); ;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.email == email);
            return user;
        }

        public async Task<UserDTO> GetConnectedUser(int userId)
        {

            var user = await context.Users.FirstOrDefaultAsync(u => u.id == userId);
            var userDto = new UserDTO
            {
                id = user.id,
                email = user.email,
                username = user.username,
                role = user.role,
                phone = user.phone,
                photo = user.photo,
                AuthCompleted = user.AuthCompleted
            };
            return userDto;

        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.username == username);
            return user;
        }

        private string CreateToken(UserDTO user)
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

        public async Task<UserAuthResult<UserDTO>> LoginAsyn(LoginUserCommand request)
        {
            var user = await GetUserByEmail(request.email);
            if (user == null)
            {
                return  UserAuthResult<UserDTO>.Fail(["Email or password is wrong or not registered"]);
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.hashedPpassword, request.password) == PasswordVerificationResult.Failed)
            {
                return UserAuthResult<UserDTO>.Fail(["Email or password is wrong or not registered"]);

            }
            var dto = new UserDTO()
            {
                id = user.id,
                email = user.email,
                phone = user.phone,
                AuthCompleted = user.AuthCompleted,
                role = user.role,
                photo = user.photo!,
                username = user.username
            };
            var token = CreateToken(dto);
            return UserAuthResult<UserDTO>.Ok(dto, token);

        }
    }
};
