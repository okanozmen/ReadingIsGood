using FluentValidation;
using ReadingIsGood.Domain.Models;

namespace ReadingIsGood.API.Validation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Id).NotEmpty().WithMessage("Customer Id cannot be empty!");
            RuleFor(customer => customer.Email).EmailAddress().WithMessage("Please enter a valid e-mail value!").When(i => !string.IsNullOrEmpty(i.Email));
            RuleFor(customer => customer.Name).NotEmpty().WithMessage("Customer Name cannot be empty!").Length(5, 20).WithErrorCode("Must be between 5 and 25 chars!");
            RuleFor(customer => customer.Phone).NotEmpty().WithMessage("Customer Phone cannot be empty!");
        }
    }
}
