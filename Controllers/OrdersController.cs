using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using DTOs.Requests.Orders;
using DTOs.Responses;
using DTOs.Responses.Orders;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase {
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService) {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrders(
        [FromQuery] int? customerId = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null) {

        try
        {
            var orders = await _orderService.GetOrdersAsync(customerId, startDate, endDate);
            return Ok(orders);
        }
        catch (Exception)
        {
            return StatusCode(500, new BaseResponse
            {
                Success = false,
                Message = "An error occurred while retrieving orders"
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponse>> GetOrder(int id) {
        try
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound(new BaseResponse
                {
                    Success = false,
                    Message = "Order not found"
                });
            }

            return Ok(order);
        }
        catch (Exception)
        {
            return StatusCode(500, new BaseResponse
            {
                Success = false,
                Message = "An error occurred while retrieving the order"
            });
        }
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse>> CreateOrder([FromBody] CreateOrderDto createOrderDto) {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    Success = false,
                    Message = "Invalid input data",
                    Data = ModelState
                });
            }

            var result = await _orderService.CreateOrderAsync(createOrderDto);
            
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetOrder), new { id = ((dynamic)result.Data).OrderId }, result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        catch (Exception)
        {
            return StatusCode(500, new BaseResponse
            {
                Success = false,
                Message = "An error occurred while creating the order"
            });
        }
    }
}
