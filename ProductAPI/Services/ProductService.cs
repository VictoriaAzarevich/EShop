using AutoMapper;
using ProductAPI.Models.Dto;
using ProductAPI.Models;
using ProductAPI.Repository;

namespace ProductAPI.Services
{
    public class ProductService(IProductRepository productRepository, IMapper mapper) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductDto> CreateProductAsync(ProductCreateUpdateDto productCreateUpdateDto)
        {
            var product = _mapper.Map<Product>(productCreateUpdateDto);
            var createdProduct = await _productRepository.CreateProductAsync(product);
            return _mapper.Map<ProductDto>(createdProduct);
        }

        public async Task DeleteProductAsync(int productId)
        {
            await _productRepository.DeleteProductAsync(productId);
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(int page, int pageSize)
        {
            var products = await _productRepository.GetProductsAsync(page, pageSize);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> UpdateProductAsync(int productId, ProductCreateUpdateDto productCreateUpdateDto)
        {
            var product = _mapper.Map<Product>(productCreateUpdateDto);
            var updatedProduct = await _productRepository.UpdateProductAsync(productId, product);
            return _mapper.Map<ProductDto>(updatedProduct);
        }
    }
}
