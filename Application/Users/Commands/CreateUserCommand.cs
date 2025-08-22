using hahn.Application.DTOs;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using MediatR;

namespace hahn.Application.Users.Commands
{
public record CreateUserCommand() : IRequest<UserAuthResult<UserDTO>> {
        public string email { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public Roles role { get; set; }
        public IFormFile? photo { get; set; } 
    }
}
