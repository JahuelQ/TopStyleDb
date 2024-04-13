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

        public async Task DeleteProduct(int id)
        {
            await _repo.DeleteProduct(id);
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

        public async Task<List<ProductDTO>> GetProductByName(string name)
        {
            var products = await _repo.GetProductByName(name);
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

        public async Task<bool> UpdateProduct(ProductDTO productDto)
        {
            var productToUpdate = new Product
            {
                ProductId = productDto.ProductId,
                ProductName = productDto.ProductName,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId
            };

            return await _repo.UpdateProduct(productToUpdate);
        }
    }
}
