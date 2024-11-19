using FluentValidation;
using Luftborn.Repositories.CustomerRepository;
using Luftborn.Services.Common;
using Luftborn.Services.CustomerService.Command;
using MediatR;
using System.Text.Json;

namespace Luftborn.Services.CustomerService.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommend, Response>
    {
        private readonly IValidator<CreateCustomerCommend> _validator;
        private readonly ICustomerRepository _customerRepository;
        public CreateCustomerCommandHandler(IValidator<CreateCustomerCommend> validator,
            ICustomerRepository customerRepository)
        {
            _validator = validator;
            _customerRepository = customerRepository;
        }
        public async Task<Response> Handle(CreateCustomerCommend request, CancellationToken cancellationToken)
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
                await _customerRepository.InsertAsync(new Data.Models.Customer()
                {
                    Address = request.Address,
                    CreatedAt = DateTime.UtcNow,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                });
                return new Response() { Succeeded = true };
            }
            catch (Exception ex) 
            {
                return new Response() { Succeeded = false , Message = ex.Message };
            }
            
        }
    }
}