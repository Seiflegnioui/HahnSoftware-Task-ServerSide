using System.Security.Claims;
using hahn.Application.DTOs;
using hahn.Application.Services;
using hahn.Application.Users.Commands;
using hahn.Application.Users.Queries;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.Users.Handlers
{
    public class LoginUserHandler( IUserRepository repository, ICreateJwtService createJwtService) : IRequestHandler<LoginUserCommand, UserAuthResult<UserDTO>>
    {
        public async  Task<UserAuthResult<UserDTO>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetUserByEmail(request.email);
            if(user == null) {
                return UserAuthResult<UserDTO>.Fail(["Email Or Password incorrect or not registred !"]);
            }
            
            try
            {
                user.VerifyPassword(request.password);
            }
            catch (System.Exception)
            {
                return UserAuthResult<UserDTO>.Fail(["Email Or Password incorrect or not registred"]);
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
            var token = createJwtService.CreateJwtToken(user);
            return UserAuthResult<UserDTO>.Ok(dto,token);
        }
    }
}