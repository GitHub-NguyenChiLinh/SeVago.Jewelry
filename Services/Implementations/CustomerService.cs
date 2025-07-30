using Repositories.Interfaces;
using Services.Interfaces;
using DTOs.Requests;
using DTOs.Responses;
using AutoMapper;
using DTOs.Requests.Customers;
using DTOs.Responses.Customers;

namespace Services.Implementations
{
    public class CustomerService : ICustomerService {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;
        private readonly IMapper _mapper;

        public CustomerService(
            ICustomerRepository customerRepository,
            ILogger<CustomerService> logger,
            IMapper mapper) {
            _customerRepository = customerRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync() {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerResponse>>(customers);
        }

        public async Task<CustomerResponse> GetCustomerByIdAsync(int id) {
            var customer = await _customerRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<BaseResponse> CreateCustomerAsync(CreateCustomerDto createDto) {
            BaseResponse result = new BaseResponse();
            try
            {
                var customer = _mapper.Map<Customer>(createDto);
                await _customerRepository.AddAsync(customer);
                result.Success = true;
                result.Message = "Customer created successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                result.Message = "Error creating customer.";
            }
            return result;
        }

        public async Task<BaseResponse> UpdateCustomerAsync(UpdateCustomerDto updateDto)
        {
            BaseResponse result = new BaseResponse();
            var customerExisting = await _customerRepository.GetByIdAsync(updateDto.CustomerId);
            if (customerExisting == null)
            {
                _logger.LogError($"Customer with ID {updateDto.CustomerId} not found.");
                result.Message = "Customer not found.";
                return result;
            }
            try
            {
                // Map only the properties that should be updated
                _mapper.Map(updateDto, customerExisting);
                customerExisting.UpdatedAt = DateTime.UtcNow;
                
                await _customerRepository.UpdateAsync(customerExisting);
                result.Success = true;
                result.Message = "Customer updated successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                result.Message = "Error updating customer.";
            }
            return result;
        }

        public async Task<BaseResponse> DeleteCustomerAsync(int id)
        {
            BaseResponse result = new BaseResponse();
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                _logger.LogError($"Customer with ID {id} not found.");
                result.Message = "Customer not found.";
                return result;
            }
            try
            {
                customer.IsDeleted = true;
                customer.UpdatedAt = DateTime.UtcNow;
                await _customerRepository.UpdateAsync(customer);
                result.Success = true;
                result.Message = "Customer deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                result.Message = "Error deleting customer.";
            }
            return result;
        }
    }
}