using Luftborn.Repositories.CustomerRepository;
using Luftborn.Services.Common;
using Luftborn.Services.CustomerService.Dtos;
using Luftborn.Services.CustomerService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.Services.CustomerService.Handlers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery ,Paginated<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public  async Task<Paginated<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var allCustomers = _customerRepository.GetAllAsNoTracking();
            var returnedData =await  allCustomers.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(c=>new CustomerDto()
                {
                    Address = c.Address,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Id = c.Id,
                    IsActive = c.IsActive,
                    PhoneNumber = c.PhoneNumber
                }).ToListAsync();

            return new Paginated<CustomerDto>
            {
                TotalCount = allCustomers.Count(),
                PageSize = request.PageSize,
                pageIndex = request.PageIndex,
                Data = returnedData
            };
        }
    }
}
