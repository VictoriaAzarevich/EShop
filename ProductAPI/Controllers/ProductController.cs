using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models.Dto;
using ProductAPI.Services;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService,
        ILogger<ProductController> logger, ICloudinaryService cloudinaryService) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly ILogger<ProductController> _logger = logger;
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService;

        [HttpGet]
        public async Task<IActionResult> GetProducts(int page = 1, int pageSize = 10, int? categoryId = null)
        {
            try
            {
                var productDtos = await _productService.GetProductsAsync(page, pageSize, categoryId);
                var totalProducts = await _productService.GetProductCountAsync(categoryId); 
                var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

                var result = new
                {
                    Products = productDtos,
                    TotalPages = totalPages
                };

                return Ok(result);
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateUpdateDto productCreateUpdateDto)
        {
            try
            {
                if (productCreateUpdateDto.Image != null)
                {
                    productCreateUpdateDto.ImageUrl = await _cloudinaryService.UploadImageAsync(productCreateUpdateDto.Image);
                }
                var productDto = await _productService.CreateProductAsync(productCreateUpdateDto);
                return CreatedAtAction(nameof(GetProductById), new { id = productDto.ProductId }, productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating product.");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductCreateUpdateDto productCreateUpdateDto)
        {
            try
            {
                if (productCreateUpdateDto.Image != null)
                {
                    productCreateUpdateDto.ImageUrl = await _cloudinaryService.UploadImageAsync(productCreateUpdateDto.Image);
                }
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

        [Authorize(Roles = "admin")]
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
