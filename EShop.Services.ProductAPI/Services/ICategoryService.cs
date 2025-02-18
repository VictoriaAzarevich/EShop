using EShop.Services.ProductAPI.Models.Dto;

namespace EShop.Services.ProductAPI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int categoryId);
        Task<CategoryDto> CreateCategoryAsync(CategoryCreateUpdateDto categoryCreateUpdateDto);
        Task<CategoryDto> UpdateCategoryAsync(int categoryId, CategoryCreateUpdateDto categoryCreateUpdateDto);
        Task DeleteCategoryAsync(int categoryId);
    }
}
