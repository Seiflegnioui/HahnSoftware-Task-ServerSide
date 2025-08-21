using hahn.Application.DTOs;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.Users.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand,CustomResult<UserDTO> >
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

public async Task<CustomResult<UserDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
{
    var duplicatedEmail = await _userRepository.GetUserByEmail(request.email);

    if (duplicatedEmail is not null)
    {
        return CustomResult<UserDTO>.Fail(new List<string>(){"email already existed!"});
    }

    var duplicatedUsername = await _userRepository.GetUserByEmail(request.email);

    if (duplicatedUsername is not null)
    {
        return CustomResult<UserDTO>.Fail(new List<string>(){"useRname already existed!"});
    }

    var user = await _userRepository.AddAsync(request);
    return user;
}

    }
}
