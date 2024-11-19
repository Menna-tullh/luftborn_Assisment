using FluentValidation;
using Luftborn.Services.CustomerService.Command;
using Luftborn.Services.Resources;
using System.Text.RegularExpressions;


namespace Luftborn.Services.CustomerService.Validators
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommend>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.FirstName)
                 .NotNull().WithMessage(Messages.EmptyData);

            RuleFor(x => x.LastName)
                 .NotNull().WithMessage(Messages.EmptyData);

            RuleFor(x => x.Address)
                 .NotNull().WithMessage(Messages.EmptyData);

            RuleFor(x => x.PhoneNumber)
            .Must(phoneNumber => string.IsNullOrEmpty(phoneNumber) || Regex.IsMatch(phoneNumber, @"^\+201[0-9]{9}$"))
            .WithMessage(Messages.InValidPhoneNumber);

            RuleFor(x => x.Email)
            .Must(email => string.IsNullOrEmpty(email) || Regex.IsMatch(email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+$"))
            .WithMessage(Messages.InValidEmailAddress);
        }
    }
}
