using FluentValidation;
using ReadingIsGood.Domain.Models;

namespace ReadingIsGood.API.Validation
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.OrderId).NotEmpty().WithMessage("Order Id cannot be empty!");
            RuleFor(order => order.CustomerId).NotEmpty().WithMessage("Customer Id cannot be empty!");
            RuleFor(x => x.Products).NotNull().Must(products => products.Count > 0).WithMessage("Products cannot be empty!");
        }
    }
}
