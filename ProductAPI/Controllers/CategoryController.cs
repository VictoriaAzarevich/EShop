using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models.Dto;
using ProductAPI.Services;

namespace ProductAPI.Controllers
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
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var categoryDto = await _categoryService.GetCategoryByIdAsync(id);
                return Ok(categoryDto);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Category with ID {categoryId} not found.", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching category with ID {categoryId}.", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryCreateUpdateDto categoryCreateUpdateDto)
        {
            try
            {
                var updatedCategoryDto = await _categoryService.UpdateCategoryAsync(id, categoryCreateUpdateDto);
                return Ok(updatedCategoryDto);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Category with ID {categoryId} not found for update.", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating category with ID {categoryId}.", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Category with ID {categoryId} not found for deletion.", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting category with ID {categoryId}.", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
