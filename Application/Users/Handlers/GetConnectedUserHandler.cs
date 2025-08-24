using System.Security.Claims;
using hahn.Application.DTOs;
using hahn.Application.Users.Queries;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.Users.Handlers
{
public class GetConnectedUserHandler(IHttpContextAccessor http, IUserRepository repository) : IRequestHandler<GetConnectedUserQuery, CustomResult<UserDTO>>
{
    public async Task<CustomResult<UserDTO>> Handle(GetConnectedUserQuery request, CancellationToken cancellationToken)
    {
        var userIdClaim = http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim))
            return CustomResult<UserDTO>.Fail(new List<string>() { "User not authenticated." });

        var userId = int.Parse(userIdClaim);
        var user = await repository.GetConnectedUser(userId);
        
        var dto = new UserDTO{
                id = user.id,
                email = user.email,
                username = user.username,
                role = user.role,
                phone = user.phone,
                photo = user.photo,
                AuthCompleted = user.AuthCompleted
            };
        return CustomResult<UserDTO>.Ok(dto);
    }
}
}