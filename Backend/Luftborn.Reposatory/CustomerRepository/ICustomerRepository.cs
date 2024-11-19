
using Luftborn.Data.Models;

namespace Luftborn.Repositories.CustomerRepository
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id);
        IQueryable<Customer> GetAllAsNoTracking();
        Task UpdateAsync(Customer entity);
        Task InsertAsync(Customer entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
