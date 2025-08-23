using System.Security.Claims;
using hahn.Application.DTOs;
using hahn.Application.Users.Commands;
using hahn.Application.Users.Queries;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.Users.Handlers
{
    public class LoginUserHandler( IUserRepository repository) : IRequestHandler<LoginUserCommand, UserAuthResult<UserDTO>>
    {
        public async  Task<UserAuthResult<UserDTO>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.LoginAsyn(request);
            return result;
        }
    }
}