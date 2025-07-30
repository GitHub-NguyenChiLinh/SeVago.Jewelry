using DTOs.Responses.Orders;

namespace Repositories.Interfaces {
    public interface IOrderRepository {
        Task<IEnumerable<Order>> GetOrdersAsync(int? customerId = null, DateTime? startDate = null, DateTime? endDate = null);
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        Task<bool> ExistsAsync(int id);
    }
}