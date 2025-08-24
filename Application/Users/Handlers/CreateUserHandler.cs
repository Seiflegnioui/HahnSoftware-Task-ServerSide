using hahn.Application.DTOs;
using hahn.Application.Services;
using hahn.Application.Users.Commands;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using MediatR;

namespace hahn.Application.Users.Handlers
{

    public class CreateUserHandler(IUserRepository _userRepository, ICreateJwtService createJwtService) : IRequestHandler<CreateUserCommand, UserAuthResult<UserDTO>>
    {
     

        public async Task<UserAuthResult<UserDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var duplicatedEmail = await _userRepository.GetUserByEmail(request.email);
            if (duplicatedEmail != null)
                return UserAuthResult<UserDTO>.Fail(new List<string> { "email already existed!" });

            var duplicatedUsername = await _userRepository.GetUserByUsername(request.username);
            if (duplicatedUsername != null)
                return UserAuthResult<UserDTO>.Fail(new List<string> { "username already existed!" });


            var folder_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "users");
            if (!Directory.Exists(folder_path))
                Directory.CreateDirectory(folder_path);

            string filename;

            if (request.photo != null && request.photo.Length > 0)
            {
                filename = request.username + "-" + Guid.NewGuid().ToString() + Path.GetExtension(request.photo.FileName);
                var filepath = Path.Combine(folder_path, filename);

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await request.photo.CopyToAsync(stream);
                }
            }
            else
            {
                filename = "default.png";
            }

            var user = User.Create(
                email: request.email,
                username: request.username,
                phone: request.phone,
                role: request.role,
                password: request.password,
                photo: filename
            );

            user = await _userRepository.AddAsync(user);
            var token = createJwtService.CreateJwtToken(user);

             var userDto = new UserDTO
            {
                id = user.id,
                email = user.email,
                username = user.username,
                role = user.role,
                phone = user.phone,
                photo = filename,
                AuthCompleted = user.AuthCompleted,
            };
            return UserAuthResult<UserDTO>.Ok(userDto, token);
        }
    }


}
