namespace Repositories.Interfaces {
    public interface IProductRepository {
      public Task<IEnumerable<Product>> GetAllAsync();
      public Task AddAsync(Product product);
      public Task<Product> GetByIdAsync(int id);
      public Task<IEnumerable<Product>> GetByIdsAsync(List<int> ids);
    }
}