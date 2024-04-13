using Microsoft.AspNetCore.Mvc;
using TopStyleDb.Core.Interfaces;
using TopStyleDb.Core.Services;
using TopStyleDb.Models.DTO;

namespace TopStyleDb.Controllers
{
    [Route("api/product/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> NewProduct(ProductDTO productDto)
        {
            var newProduct = await _service.CreateProduct(productDto);
            if (newProduct == null)
            {
                return BadRequest("Unable to create product.");
            }
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ProductId }, newProduct);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _service.GetProductById(id);
            if (product == null)
            {
                return NotFound("No product with that ID.");
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("get/all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _service.GetAllProducts();
            if (products == null || products.Count == 0)
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }

        [HttpGet]
        [Route("get/search/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var products = await _service.GetProductByName(name);
            if (products == null || products.Count == 0)
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateProduct(ProductDTO product)
        {
            bool updatedProduct = await _service.UpdateProduct(product);
            if (!updatedProduct)
            {
                return BadRequest("Unable to update product.");
            }
            return Ok($"{product.ProductName}, has been updated.");
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deletedProduct = await _service.GetProductById(id);
            if (deletedProduct == null) 
            { 
                return NotFound("No product with that Id.");
            }
            await _service.DeleteProduct(id);
            return Ok(deletedProduct.ProductName + ", has been deleted.");
        }
    }
}
