using FluentValidation;
using hahn.Application.Product.Commands;
using hahn.Domain.Enums;

namespace hahn.Application.Product.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.description)
                .NotEmpty().WithMessage("Product description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.category)
                .IsInEnum().WithMessage("Invalid product category.");

            RuleFor(x => x.image)
                .NotNull().WithMessage("Product image is required.");

            RuleFor(x => x.price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");
        }
    }
}
