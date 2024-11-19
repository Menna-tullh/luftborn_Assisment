using Luftborn.Repositories.CustomerRepository;
using Luftborn.Services.Common;
using Luftborn.Services.CustomerService.Commands;
using MediatR;

namespace Luftborn.Services.CustomerService.Handlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Response>
    {
        private readonly ICustomerRepository _customerRepository;
        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Response> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _customerRepository.DeleteAsync(Guid.Parse(request.Id));
                return new Response() { Succeeded = result };
            }
            catch (Exception ex)
            {
                return new Response() { Succeeded = false, Message = ex.Message };
            }
        }
    }
}
