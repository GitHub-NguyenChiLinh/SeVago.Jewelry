using DTOs.Requests;
using DTOs.Requests.Customers;
using DTOs.Responses;
using DTOs.Responses.Customers;

namespace Services.Interfaces
{

    public interface ICustomerService {
        Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync();
        Task<CustomerResponse> GetCustomerByIdAsync(int id);
        Task<BaseResponse> CreateCustomerAsync(CreateCustomerDto createDto);
        Task<BaseResponse> UpdateCustomerAsync(UpdateCustomerDto updateDto);
        Task<BaseResponse> DeleteCustomerAsync(int id);
    }
}