using FluentValidation;
using Luftborn.Repositories.CustomerRepository;
using Luftborn.Services.Common;
using Luftborn.Services.CustomerService.Commands;
using Luftborn.Services.Resources;
using MediatR;
using System.Text.Json;

namespace Luftborn.Services.CustomerService.Handlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response>
    {
        private readonly IValidator<UpdateCustomerCommand> _validator;
        private readonly ICustomerRepository _customerRepository;
        public UpdateCustomerCommandHandler(IValidator<UpdateCustomerCommand> validator,
            ICustomerRepository customerRepository)
        {
            _validator = validator;
            _customerRepository = customerRepository;
        }
        public async Task<Response> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            //validate input data
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return new Response()
                {
                    Succeeded = false,
                    Errors = validationResult.Errors.Select(e => JsonSerializer.Serialize(e.ErrorMessage)).ToList()
                };
            }

            //try to create data
            try
            {
                var customer = await _customerRepository.GetByIdAsync(Guid.Parse(request.Id));
                if(customer == null)
                    return new Response() { Succeeded = false, Message = Messages.InValidId };

                customer.FirstName = request.FirstName;
                customer.LastName = request.LastName;
                customer.Email = request.Email?? customer.Email;
                customer.PhoneNumber = request.PhoneNumber ?? customer.Email;
                customer.IsActive = request.IsActive;
                customer.Address = request.Address;

                await _customerRepository.UpdateAsync(customer);
                return new Response() { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new Response() { Succeeded = false, Message = ex.Message };
            }
        }
    }
}
