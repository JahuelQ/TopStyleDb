using TopStyleDb.Models.Entities;

namespace TopStyleDb.Data.Interfaces
{
    public interface ICategoryRepo
    {
        public Task<Category> CreateCategory(Category category);
        public Task DeleteCategory(int categoryId);
        public Task<List<Category>> GetAllCategories();
        public Task<Category> GetCategory(int categoryId);
    }
}
