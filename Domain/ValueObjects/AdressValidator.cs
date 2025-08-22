using FluentValidation;
using hahn.Domain.ValueObject;

public class AdressValidator : AbstractValidator<Adress
>
{
    public AdressValidator()
    {

        
        RuleFor(x => x.country)
            .NotEmpty().WithMessage("Street is required");

        RuleFor(x => x.city)
            .NotEmpty().WithMessage("City is required");

        RuleFor(x => x.adress)
            .NotEmpty().WithMessage("adress is required")
            .WithMessage("asress must be 5 digits");
    }
}
