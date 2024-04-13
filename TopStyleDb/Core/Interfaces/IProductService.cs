using TopStyleDb.Models.DTO;
using TopStyleDb.Models.Entities;

namespace TopStyleDb.Core.Interfaces
{
    public interface IProductService
    {
        public Task<ProductDTO> CreateProduct(ProductDTO productDto);
        public Task<ProductDTO> GetProductById(int id);
        public Task<List<ProductDTO>> GetAllProducts();
        public Task<List<ProductDTO>> GetProductByName(string name);
        public Task<bool> UpdateProduct(ProductDTO productDto);
        public Task DeleteProduct(int id);
    }
}
