using FluentValidation;
using hahn.Application.DTOs;
using hahn.Application.Users.Commands;
using hahn.Domain.Entities;
using MediatR;

namespace hahn.Application.Users.Validators
{
 public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(50);

            RuleFor(x => x.phone)
                .NotEmpty().WithMessage("Phone is required");

            RuleFor(x => x.password).MaximumLength(50).WithMessage("Max Length is 50")
            .MinimumLength(8).WithMessage("min length is 8");

            RuleFor(x => x.role)
                .IsInEnum().WithMessage("Invalid role");
        }
    }
}