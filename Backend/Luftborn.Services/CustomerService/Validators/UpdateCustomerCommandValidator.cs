using FluentValidation;
using Luftborn.Services.CustomerService.Command;
using Luftborn.Services.CustomerService.Commands;
using Luftborn.Services.Resources;
using System.Text.RegularExpressions;


namespace Luftborn.Services.CustomerService.Validators
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Id)
               .NotNull().Must(id=>IsValidGuid(id)).WithMessage(Messages.InValidId);

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
        private bool IsValidGuid(string guidString)
        {
            Guid guid;
            return Guid.TryParse(guidString, out guid);
        }
    }
    
}
