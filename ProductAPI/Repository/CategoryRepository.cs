using Microsoft.EntityFrameworkCore;
using ProductAPI.DBContext;
using ProductAPI.Models;

namespace ProductAPI.Repository
{
    public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(p => p.CategoryId == categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(p => p.CategoryId == categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            return category;
        }

        public async Task<Category> UpdateCategoryAsync(int categoryId, Category updatedCategory)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(p => p.CategoryId == categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            category.CategoryName = updatedCategory.CategoryName;
            await _dbContext.SaveChangesAsync();
            return category;
        }
    }
}
