using Microsoft.EntityFrameworkCore;
using ProductAPI.DBContext;
using ProductAPI.Models;

namespace ProductAPI.Repository
{
    public class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Product> CreateProductAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var product = await _dbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            return product;
        }

        public async Task<int> GetProductCountAsync(int? categoryId)
        {
            var query = _dbContext.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            return await query.CountAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int page, int pageSize, int? categoryId)
        {
            var query = _dbContext.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Product> UpdateProductAsync(int productId, Product updatedProduct)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Description = updatedProduct.Description;
            product.CategoryId = updatedProduct.CategoryId;
            product.ImageUrl = updatedProduct.ImageUrl;
            await _dbContext.SaveChangesAsync();

            return product;
        }
    }
}
