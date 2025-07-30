using Repositories.Interfaces;
using Services.Interfaces;
using DTOs.Requests.Orders;
using DTOs.Responses;
using DTOs.Responses.Orders;
using AutoMapper;

namespace Services.Implementations {
    public class OrderService : IOrderService {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            ILogger<OrderService> logger,
            IMapper mapper) {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersAsync(int? customerId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersAsync(customerId, startDate, endDate);
                var orderResponses = new List<OrderResponse>();

                foreach (var order in orders)
                {
                    var orderResponse = _mapper.Map<OrderResponse>(order);
                    orderResponse.CustomerName = order.Customer?.FullName ?? "Unknown";
                    
                    foreach (var detail in orderResponse.OrderDetails)
                    {
                        detail.ProductName = order.OrderDetails.FirstOrDefault(od => od.ProductId == detail.ProductId)?.Product?.Name ?? "Unknown";
                        detail.SubTotal = detail.Quantity * detail.UnitPrice;
                    }
                    
                    orderResponses.Add(orderResponse);
                }

                return orderResponses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting orders");
                throw;
            }
        }

        public async Task<OrderResponse> GetOrderByIdAsync(int id)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return null;
                }

                var orderResponse = _mapper.Map<OrderResponse>(order);
                orderResponse.CustomerName = order.Customer?.FullName ?? "Unknown";
                
                foreach (var detail in orderResponse.OrderDetails)
                {
                    detail.ProductName = order.OrderDetails.FirstOrDefault(od => od.ProductId == detail.ProductId)?.Product?.Name ?? "Unknown";
                    detail.SubTotal = detail.Quantity * detail.UnitPrice;
                }

                return orderResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting order with id: {OrderId}", id);
                throw;
            }
        }

        public async Task<BaseResponse> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            try
            {
                // Validate customer exists
                var customer = await _customerRepository.GetByIdAsync(createOrderDto.CustomerId);
                if (customer == null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Customer not found"
                    };
                }

                var productIds = createOrderDto.OrderDetails.Select(d => d.ProductId).Distinct().ToList();
                var products = await _productRepository.GetByIdsAsync(productIds);
                
                if (products.Count() != productIds.Count)
                {
                    var foundProductIds = products.Select(p => p.ProductId).ToList();
                    var missingProductIds = productIds.Except(foundProductIds).ToList();
                    return new BaseResponse
                    {
                        Success = false,
                        Message = $"Products with IDs {string.Join(", ", missingProductIds)} not found"
                    };
                }

                decimal totalAmount = createOrderDto.OrderDetails.Sum(detail => detail.Quantity * detail.UnitPrice);

                // Create order
                var order = new Order
                {
                    CustomerId = createOrderDto.CustomerId,
                    OrderDate = createOrderDto.OrderDate,
                    TotalAmount = totalAmount,
                    OrderDetails = createOrderDto.OrderDetails.Select(detail => new OrderDetail
                    {
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        UnitPrice = detail.UnitPrice
                    }).ToList()
                };

                var createdOrder = await _orderRepository.CreateOrderAsync(order);

                return new BaseResponse
                {
                    Success = true,
                    Message = "Order created successfully",
                    Data = new { OrderId = createdOrder.OrderId }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating order");
                return new BaseResponse
                {
                    Success = false,
                    Message = "An error occurred while creating the order"
                };
            }
        }
    }
}