using EShop.Services.ProductAPI.Models.Dto;

namespace EShop.Services.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts(int page, int pageSize);
        Task<ProductDto> GetProductById(int productId);
        Task<ProductDto> CreateProduct(ProductCreateUpdateDto productCreateUpdateDto);
        Task<ProductDto> UpdateProduct(int productId, ProductCreateUpdateDto productCreateUpdateDto);
        Task DeleteProduct(int productId);
    }
}
