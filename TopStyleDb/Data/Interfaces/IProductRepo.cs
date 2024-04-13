using TopStyleDb.Models.Entities;

namespace TopStyleDb.Data.Interfaces
{
    public interface IProductRepo
    {
        public Task<Product> CreateProduct(Product product);
        public Task<Product> GetProductById(int id);
        public Task<List<Product>> GetAllProducts();
        public Task<List<Product>> GetProductByName(string name);
        public Task<bool> UpdateProduct(Product product);
        public Task DeleteProduct(int id);
    }
}
