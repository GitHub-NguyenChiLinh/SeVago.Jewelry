using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Implementation {
    public class ProductRepository : BaseRepository, IProductRepository {

        public ProductRepository(BaseDbContext context) : base(context) { }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Where(x => !x.IsDeleted)
                .ToListAsync();
        }
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id && !x.IsDeleted);
        }

        public async Task<IEnumerable<Product>> GetByIdsAsync(List<int> ids)
        {
            return await _context.Products
                .Where(x => ids.Contains(x.ProductId) && !x.IsDeleted)
                .ToListAsync();
        }
    }
}