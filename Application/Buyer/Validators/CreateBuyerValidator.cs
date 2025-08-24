using System.Security.Claims;
using FluentValidation;
using hahn.Application.buyer.Commands;

namespace hahn.Application.buyer.Validators
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