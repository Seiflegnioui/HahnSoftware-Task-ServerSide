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
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.email == email);
            return user;
        }
        public async Task<User> GetConnectedUser(int userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.id == userId);
            return user;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.username == username);
            return user;
        }

        public async Task<User> AddAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

    }
};
