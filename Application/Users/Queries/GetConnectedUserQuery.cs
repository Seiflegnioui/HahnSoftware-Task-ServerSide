using hahn.Application.DTOs;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.Users.Queries
{
    public class GetConnectedUserQuery : IRequest<CustomResult<UserDTO>>
    {

    }
}