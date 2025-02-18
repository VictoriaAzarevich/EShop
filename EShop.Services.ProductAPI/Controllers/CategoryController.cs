using EShop.Services.ProductAPI.Models.Dto;
using EShop.Services.ProductAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;
        private readonly ILogger<CategoryController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categoryDtos = await _categoryService.GetCategoriesAsync();
                return Ok(categoryDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching categories.");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            try
            {
                var categoryDto = await _categoryService.GetCategoryByIdAsync(categoryId);
                return Ok(categoryDto);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Category with ID {categoryId} not found.", categoryId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching category with ID {categoryId}.", categoryId);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateUpdateDto categoryCreateUpdateDto)
        {
            try
            {
                var categoryDto = await _categoryService.CreateCategoryAsync(categoryCreateUpdateDto);
                return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDto.CategoryId }, categoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating category.");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryCreateUpdateDto categoryCreateUpdateDto)
        {
            try
            {
                var updatedCategoryDto = await _categoryService.UpdateCategoryAsync(categoryId, categoryCreateUpdateDto);
                return Ok(updatedCategoryDto);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Category with ID {categoryId} not found for update.", categoryId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating category with ID {categoryId}.", categoryId);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(categoryId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Category with ID {categoryId} not found for deletion.", categoryId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting category with ID {categoryId}.", categoryId);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
