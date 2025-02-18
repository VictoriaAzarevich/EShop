using EShop.Services.ProductAPI.Models.Dto;

namespace EShop.Services.ProductAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(int page, int pageSize);
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<ProductDto> CreateProductAsync(ProductCreateUpdateDto productCreateUpdateDto);
        Task<ProductDto> UpdateProductAsync(int productId, ProductCreateUpdateDto productCreateUpdateDto);
        Task DeleteProductAsync(int productId);
    }
}
