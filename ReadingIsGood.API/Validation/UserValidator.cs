using FluentValidation;
using ReadingIsGood.Domain.Models;
using System.Text.RegularExpressions;

namespace ReadingIsGood.API.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Id).NotEmpty().WithMessage("User Id cannot be empty!");
            RuleFor(user => user.Email).EmailAddress().WithMessage("Please enter a valid e-mail value!").When(i => !string.IsNullOrEmpty(i.Email));
            RuleFor(user => user.Password)
            .NotEmpty().WithMessage("User Password cannot be empty!")
            .Must(IsPasswordValid).WithMessage("Your password must contain at least eight characters, at least one letter and one number!")
            .Must(PasswordLength).WithMessage("Password Length must be equal or greater than 8");
        }

        private bool IsPasswordValid(string arg)
        {
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            return regex.IsMatch(arg);
        }
        private bool PasswordLength(string pass)
        {
            if (pass.Length < 8)
            {
                return false;
            }

            return true;
        }
    }
}
