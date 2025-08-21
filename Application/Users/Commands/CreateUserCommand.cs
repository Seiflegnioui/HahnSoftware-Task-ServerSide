using hahn.Application.DTOs;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using MediatR;

namespace hahn.Application.Users.Commands
{
public record CreateUserCommand() : IRequest<CustomResult<UserDTO>>    {
        public string email { get; set; }
        public string username { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public Roles role { get; set; }
        public IFormFile? photo { get; set; } 
    }
}
