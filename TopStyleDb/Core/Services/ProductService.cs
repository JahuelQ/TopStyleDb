using TopStyleDb.Core.Interfaces;
using TopStyleDb.Data.Interfaces;
using TopStyleDb.Data.Repos;
using TopStyleDb.Models.DTO;
using TopStyleDb.Models.Entities;

namespace TopStyleDb.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _repo;
        public ProductService(IProductRepo repo)
        {
            _repo = repo;
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO productDto)
        {
            var product = new Product
            {
                ProductName = productDto.ProductName,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId
            };

            var newProduct = await _repo.CreateProduct(product);
            if (newProduct == null)
            {
                return null;
            }

            return new ProductDTO
            {
                ProductId = newProduct.ProductId,
                ProductName = newProduct.ProductName,
                Description = newProduct.Description,
                Price = newProduct.Price,
                CategoryId = newProduct.CategoryId
            };
        }

        public Task<Product> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDTO>> GetAllProducts()
        {
            var products = await _repo.GetAllProducts();
            if (products == null)
            {
                return null;
            }

            return products.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId
            }).ToList();
        }

        public Task<List<Product>> GetProductByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            var product = await _repo.GetProductById(id);
            if (product == null)
            {
                return null;
            }

            return new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
        }

        public Task<List<ProductDTO>> GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateProduct(ProductDTO productDto)
        {
            var productToUpdate = await _repo.GetProductById(productDto.ProductId);

            if (productToUpdate == null)
            {
                return false;
            }

            productToUpdate.ProductName = productDto.ProductName;
            productToUpdate.Description = productDto.Description;
            productToUpdate.Price = productDto.Price;
            productToUpdate.CategoryId = productDto.CategoryId;

            return await _repo.UpdateProduct(productToUpdate);
        }
    }
}
