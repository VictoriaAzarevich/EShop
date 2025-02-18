using AutoMapper;
using EShop.Services.ProductAPI.DbContexts;
using EShop.Services.ProductAPI.Models;
using EShop.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace EShop.Services.ProductAPI.Repository
{
    public class CategoryRepository(ApplicationDbContext dbContext, IMapper mapper) : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<CategoryDto> CreateCategory(CategoryCreateUpdateDto categoryCreateUpdateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateUpdateDto);

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteCategory(int categoryId)
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

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryById(int categoryId)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(p => p.CategoryId == categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategory(int categoryId, CategoryCreateUpdateDto categoryCreateUpdateDto)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(p => p.CategoryId == categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            _mapper.Map(categoryCreateUpdateDto, category);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
