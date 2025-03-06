using AutoMapper;
using ProductAPI.Models.Dto;
using ProductAPI.Models;

namespace ProductAPI.Repository
{
    public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateUpdateDto categoryCreateUpdateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateUpdateDto);
            var createdCategory = await _categoryRepository.CreateCategoryAsync(category);
            return _mapper.Map<CategoryDto>(createdCategory);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            await _categoryRepository.DeleteCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int categoryId, CategoryCreateUpdateDto categoryCreateUpdateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateUpdateDto);
            var updatedCategory = await _categoryRepository.UpdateCategoryAsync(categoryId, category);
            return _mapper.Map<CategoryDto>(updatedCategory);
        }
    }
}
