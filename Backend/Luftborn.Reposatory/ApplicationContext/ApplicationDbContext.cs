using Luftborn.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.Repositories.ApplicationContext
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
