using hahn.Application.DTOs;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Enums;
using MediatR;

namespace hahn.Application.Users.Commands
{
public record LoginUserCommand() : IRequest<UserAuthResult<UserDTO>> {
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    
    }
}
