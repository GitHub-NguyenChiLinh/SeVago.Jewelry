namespace Repositories.Interfaces {
    public interface ICustomerRepository {
      
      public Task<IEnumerable<Customer>> GetAllAsync();
      public Task AddAsync(Customer customer);
      public Task UpdateAsync(Customer customer);
      public Task<Customer> GetByIdAsync(int id);

    }
}