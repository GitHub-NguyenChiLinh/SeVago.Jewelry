using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Implementation {
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {

        public CustomerRepository(BaseDbContext context) : base(context) { }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.Where(x => !x.IsDeleted)
                .ToListAsync();
        }
        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == id && !x.IsDeleted);
        }
    }
}