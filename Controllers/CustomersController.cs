using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using DTOs.Requests;
using DTOs.Responses;
using DTOs.Requests.Customers;
using DTOs.Responses.Customers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase {
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService) {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetCustomers() {
        var result = await _customerService.GetAllCustomersAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerResponse>> GetCustomer(int id) {
        var result = await _customerService.GetCustomerByIdAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse>> CreateCustomer([FromBody] CreateCustomerDto createDto) {
        var result = await _customerService.CreateCustomerAsync(createDto);
        if (result.Success)
            return CreatedAtAction(nameof(GetCustomer), new { id = 0 }, result);
        return BadRequest(result);
    }

    [HttpPut]
    public async Task<ActionResult<BaseResponse>> UpdateCustomer([FromBody] UpdateCustomerDto updateDto) {
        var result = await _customerService.UpdateCustomerAsync(updateDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse>> DeleteCustomer(int id) {
        var result = await _customerService.DeleteCustomerAsync(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
