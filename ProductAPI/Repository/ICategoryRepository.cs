using ProductAPI.Models;

namespace ProductAPI.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(int categoryId, Category updatedCategory);
        Task DeleteCategoryAsync(int categoryId);
    }
}
