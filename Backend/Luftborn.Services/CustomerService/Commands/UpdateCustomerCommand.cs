using Luftborn.Services.Common;
using MediatR;


namespace Luftborn.Services.CustomerService.Commands
{
    public class UpdateCustomerCommand : IRequest<Response>
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
    }
}
