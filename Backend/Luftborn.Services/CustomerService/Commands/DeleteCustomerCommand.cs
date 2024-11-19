using Luftborn.Services.Common;
using MediatR;

namespace Luftborn.Services.CustomerService.Commands
{
    public class DeleteCustomerCommand : IRequest<Response>
    {
        public string Id { get; set; } = null!;
    }
}
