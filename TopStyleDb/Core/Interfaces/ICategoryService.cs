using TopStyleDb.Models.Entities;
using TopStyleDb.Models.DTO;

namespace TopStyleDb.Core.Interfaces
{
    public interface ICategoryService
    {
        public Task<CreateCategoryDTO> CreateCategory(CreateCategoryDTO categoryDto);
        public Task DeleteCategory(int categoryId);
        public Task<List<CategoryDTO>> GetAllCategories();
        public Task<CategoryContentDTO> GetCategoryContent(int categoryId);
        public Task<Category> GetCategoryById(int categoryId);
    }
}
