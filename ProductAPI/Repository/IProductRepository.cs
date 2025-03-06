using ProductAPI.Models;

namespace ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(int page, int pageSize);
        Task<Product> GetProductByIdAsync(int productId);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(int productId, Product product);
        Task DeleteProductAsync(int productId);
    }
}
