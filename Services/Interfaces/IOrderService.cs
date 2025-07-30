using DTOs.Requests.Orders;
using DTOs.Responses;
using DTOs.Responses.Orders;

namespace Services.Interfaces {
    public interface IOrderService {
        Task<IEnumerable<OrderResponse>> GetOrdersAsync(int? customerId = null, DateTime? startDate = null, DateTime? endDate = null);
        Task<OrderResponse> GetOrderByIdAsync(int id);
        Task<BaseResponse> CreateOrderAsync(CreateOrderDto createOrderDto);
    }
}