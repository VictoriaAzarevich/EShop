using EShop.Services.ProductAPI.Models.Dto;
using EShop.Services.ProductAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService, ILogger<ProductController> logger) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly ILogger<ProductController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetProducts(int page = 1, int pageSize = 10)
        {
            try
            {
                var productDtos = await _productService.GetProductsAsync(page, pageSize);
                return Ok(productDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching products.");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var productDto = await _productService.GetProductByIdAsync(id);
                return Ok(productDto);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Product with ID {productId} not found.", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching product with ID {productId}.", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateUpdateDto productCreateUpdateDto)
        {
            try
            {
                var productDto = await _productService.CreateProductAsync(productCreateUpdateDto);
                return CreatedAtAction(nameof(GetProductById), new { id = productDto.ProductId }, productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating product.");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductCreateUpdateDto productCreateUpdateDto)
        {
            try
            {
                var updatedProductDto = await _productService.UpdateProductAsync(id, productCreateUpdateDto);
                return Ok(updatedProductDto);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Product with ID {productId} not found for update.", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product with ID {productId}.", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Product with ID {productId} not found for deletion.", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product with ID {productId}.", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
