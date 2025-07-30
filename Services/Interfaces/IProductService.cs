using DTOs.Requests.Products;
using DTOs.Responses;
using DTOs.Responses.Products;

namespace Services.Interfaces {
   
    public interface IProductService {
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
        Task<BaseResponse> CreateProductAsync(CreateProductDto createDto);

    }
}