using FluentValidation;
using hahn.Application.seller.Commands;

namespace hahn.Application.seller.Validators
{
    public class CreateSellerValidator : AbstractValidator<CreateSellerCommand>
    {
        public CreateSellerValidator()
        {
            RuleFor(x => x.adress)
                .NotNull().WithMessage("Address is required")
                .SetValidator(new AdressValidator()!)
                .When(x => x.adress != null);

            RuleFor(x => x.localAdress)
                .SetValidator(new AdressValidator()!)
                .When(x => x.hasLocal && x.localAdress != null);

            RuleFor(x => x.shopName)
                .NotEmpty().WithMessage("Shop name is required")
                .MaximumLength(100).WithMessage("Shop name cannot exceed 100 characters");


                RuleFor(x => x.shopDescription)
                    .NotEmpty()
                    .WithName("Shop description")
                    .WithMessage("{PropertyName} is required");




            RuleFor(x => x.field)
                .NotEmpty().WithMessage("Business field is required");

            RuleFor(x => x.personalSite)
                .Must(link => string.IsNullOrEmpty(link) || Uri.IsWellFormedUriString(link, UriKind.Absolute))
                .WithMessage("Personal site must be a valid URL");

            RuleFor(x => x.facebook)
                .Must(link => string.IsNullOrEmpty(link) || Uri.IsWellFormedUriString(link, UriKind.Absolute))
                .WithMessage("Facebook must be a valid URL");

            RuleFor(x => x.instagram)
                .Must(link => string.IsNullOrEmpty(link) || Uri.IsWellFormedUriString(link, UriKind.Absolute))
                .WithMessage("Instagram must be a valid URL");
        }
    }
}
