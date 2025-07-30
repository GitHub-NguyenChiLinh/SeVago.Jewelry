using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using DTOs.Responses.Customers;
using DTOs.Requests.Customers;
using DTOs.Responses;

public class CustomersControllerTests
{
    private readonly Mock<ICustomerService> _mockService;
    private readonly CustomersController _controller;

    public CustomersControllerTests()
    {
        _mockService = new Mock<ICustomerService>();
        _controller = new CustomersController(_mockService.Object);
    }

    [Fact]
    public async Task GetCustomers_ReturnsOkResult_WithListOfCustomers()
    {
        // Arrange
        var customers = new List<CustomerResponse> { new CustomerResponse { CustomerId = 1, FullName = "Test" } };
        _mockService.Setup(s => s.GetAllCustomersAsync()).ReturnsAsync(customers);

        // Act
        var result = await _controller.GetCustomers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(customers, okResult.Value);
    }

    [Fact]
    public async Task GetCustomer_ReturnsNotFound_WhenCustomerIsNull()
    {
        _mockService.Setup(s => s.GetCustomerByIdAsync(1)).ReturnsAsync((CustomerResponse)null);

        var result = await _controller.GetCustomer(1);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetCustomer_ReturnsOk_WhenCustomerExists()
    {
        var customer = new CustomerResponse { CustomerId = 1, FullName = "Test" };
        _mockService.Setup(s => s.GetCustomerByIdAsync(1)).ReturnsAsync(customer);

        var result = await _controller.GetCustomer(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(customer, okResult.Value);
    }

    [Fact]
    public async Task CreateCustomer_ReturnsCreatedAtAction_WhenSuccess()
    {
        var dto = new CreateCustomerDto();
        var response = new BaseResponse { Success = true };
        _mockService.Setup(s => s.CreateCustomerAsync(dto)).ReturnsAsync(response);

        var result = await _controller.CreateCustomer(dto);

        Assert.IsType<CreatedAtActionResult>(result.Result);
    }

    [Fact]
    public async Task CreateCustomer_ReturnsBadRequest_WhenFail()
    {
        var dto = new CreateCustomerDto();
        var response = new BaseResponse { Success = false };
        _mockService.Setup(s => s.CreateCustomerAsync(dto)).ReturnsAsync(response);

        var result = await _controller.CreateCustomer(dto);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
}