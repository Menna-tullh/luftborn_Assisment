using Luftborn.Services.Common;
using Luftborn.Services.CustomerService.Dtos;
using MediatR;

namespace Luftborn.Services.CustomerService.Queries
{
    public class GetAllCustomersQuery : IRequest<Paginated<CustomerDto>>
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
