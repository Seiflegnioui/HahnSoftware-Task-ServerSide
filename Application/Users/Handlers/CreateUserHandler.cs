using hahn.Application.DTOs;
using hahn.Application.Users.Commands;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.Users.Handlers
{

    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserAuthResult<UserDTO>>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserAuthResult<UserDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var duplicatedEmail = await _userRepository.GetUserByEmail(request.email);
            if (duplicatedEmail != null)
                return UserAuthResult<UserDTO>.Fail(new List<string> { "email already existed!" });

            var duplicatedUsername = await _userRepository.GetUserByUsername(request.username);
            if (duplicatedUsername != null)
                return UserAuthResult<UserDTO>.Fail(new List<string> { "username already existed!" });

            var user = await _userRepository.AddAsync(request);
            return user;
        }
    }


}
