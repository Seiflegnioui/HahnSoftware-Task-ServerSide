using System.Security.Claims;
using FluentValidation;
using hahn.Application.Buyer.Commands;
using hahn.Application.DTOs;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Repositories;
using hahn.Domain.ValueObject;
using MediatR;

namespace hahn.Application.Buyer.Validators
{
    public class CreateBuyerValidator : AbstractValidator<CreateBuyerCommand>
    {
        public CreateBuyerValidator()
        {
            RuleFor(x => x.adress).SetValidator(new AdressValidator());
            RuleFor(x => x.bio).NotEmpty().MaximumLength(300).WithMessage("max length is 300 character");
        }
    }
}