using ProductAPI.Models.Dto;

namespace ProductAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(int page, int pageSize, int? categoryId);
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<ProductDto> CreateProductAsync(ProductCreateUpdateDto productCreateUpdateDto);
        Task<ProductDto> UpdateProductAsync(int productId, ProductCreateUpdateDto productCreateUpdateDto);
        Task DeleteProductAsync(int productId);
        Task<int> GetProductCountAsync(int? categoryId);
    }
}
