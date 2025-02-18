using EShop.Services.ProductAPI.Models.Dto;

namespace EShop.Services.ProductAPI.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task<CategoryDto> GetCategoryById(int categoryId);
        Task<CategoryDto> CreateCategory(CategoryCreateUpdateDto categoryCreateUpdateDto);
        Task<CategoryDto> UpdateCategory(int categoryId, CategoryCreateUpdateDto categoryCreateUpdateDto);
        Task DeleteCategory(int categoryId);
    }
}
