using FluentValidation;
using hahn.Application.Order.Commands;

namespace hahn.Application.Order.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.buyerId)
                .GreaterThan(0).WithMessage("BuyerId must be greater than 0.");

            RuleFor(x => x.productId)
                .GreaterThan(0).WithMessage("ProductId must be greater than 0.");

            RuleFor(x => x.quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Quantity cannot exceed 100.");
        }
    }
}
