using AutoMapper;
using EShop.Services.ProductAPI.DbContexts;
using EShop.Services.ProductAPI.Models;
using EShop.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace EShop.Services.ProductAPI.Repository
{
    public class ProductRepository(ApplicationDbContext dbContext, IMapper mapper) : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductDto> CreateProduct(ProductCreateUpdateDto productCreateUpdateDto)
        {
            var product = _mapper.Map<Product>(productCreateUpdateDto);

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task DeleteProduct(int productId)
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

        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await _dbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(int page, int pageSize)
        {
            var products = await _dbContext.Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(p => p.Category)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> UpdateProduct(int productId, ProductCreateUpdateDto productCreateUpdateDto)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            _mapper.Map(productCreateUpdateDto, product);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }
    }
}
