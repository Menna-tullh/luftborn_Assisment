using Luftborn.Services.Common;
using MediatR;

namespace Luftborn.Services.CustomerService.Command
{
    public class CreateCustomerCommend : IRequest<Response>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

    }
}
