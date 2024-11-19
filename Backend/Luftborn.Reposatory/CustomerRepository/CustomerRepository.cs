using Luftborn.Data.Models;
using Luftborn.Repositories.ApplicationContext;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.Repositories.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Customer> GetAllAsNoTracking()
        {
            return _context.Customers.Where(c=>!c.IsDeleted).AsNoTracking();
        }
        public async Task<Customer?> GetByIdAsync(Guid id) 
        {
            if (id != Guid.Empty)
            {
                return await _context.Customers.FirstOrDefaultAsync(o => o.Id == id);
            }
            return null;
        }

        public async Task InsertAsync(Customer entity)
        {
            await _context.Customers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Customers.FindAsync(id);
            if (entity == null)
                return false;

            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
