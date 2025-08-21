
using hahn.Application.DTOs;
using hahn.Application.Users.Commands;
using hahn.Application.Validators;
using hahn.Domain.Entities;

namespace hahn.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<CustomResult<UserDTO>> AddAsync(CreateUserCommand request);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
    }
}