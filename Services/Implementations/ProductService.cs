using AutoMapper;
using DTOs.Requests.Products;
using DTOs.Responses;
using DTOs.Responses.Products;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations {
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;


        public ProductService
            (
            IProductRepository productRepository,
            ILogger<CustomerService> logger,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }
       
        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync() {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<BaseResponse> CreateProductAsync(CreateProductDto createDto)
        {
            BaseResponse result = new BaseResponse();
            try
            {
                var customer = _mapper.Map<Product>(createDto);
                await _productRepository.AddAsync(customer);
                result.Success = true;
                result.Message = "Product created successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                result.Message = "Error creating product.";
            }
            return result;
        }

    }
}