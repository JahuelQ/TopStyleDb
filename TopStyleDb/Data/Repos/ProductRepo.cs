using Microsoft.EntityFrameworkCore;
using TopStyleDb.Data.Interfaces;
using TopStyleDb.Models.Entities;

namespace TopStyleDb.Data.Repos
{
    public class ProductRepo : IProductRepo
    {
        private readonly TopStyleDbContext _context;
        public ProductRepo(TopStyleDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(int id)
        {
            var recipe = await _context.Products.FindAsync(id);
            _context.Products.Remove(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public Task<List<Product>> GetProductByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetProductByName(string name)
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var existingUser = await _context.Products.FindAsync();
            if (existingUser == null)
            {
                return false;
            }

            _context.Entry(existingUser).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
