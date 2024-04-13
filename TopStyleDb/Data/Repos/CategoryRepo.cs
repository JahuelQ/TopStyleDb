using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TopStyleDb.Data.Interfaces;
using TopStyleDb.Models.Entities;

namespace TopStyleDb.Data.Repos
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly TopStyleDbContext _context;
        public CategoryRepo(TopStyleDbContext context)
        {
            _context = context;
        }


        public async Task<Category> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories
            .Where(c => c.ParentCategoryId == null)
            .Include(c => c.ChildCategories)
            .ToListAsync();
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }
    }
}
