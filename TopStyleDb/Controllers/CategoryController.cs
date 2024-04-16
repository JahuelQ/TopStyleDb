using Microsoft.AspNetCore.Mvc;
using TopStyleDb.Core.Interfaces;
using TopStyleDb.Models.DTO;

namespace TopStyleDb.Controllers
{
    [Route("api/category/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService categoryService)
        {
            _service = categoryService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO categoryDto)
        {
            var newCategory = await _service.CreateCategory(categoryDto);
            if (newCategory == null)
            {
                return BadRequest("Unable to create category.");
            }
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.CategoryId }, newCategory);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _service.DeleteCategory(id);
            return Ok("Category deleted.");
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categoriesDto = await _service.GetAllCategories();
            if (categoriesDto == null || categoriesDto.Count == 0)
            {
                return NotFound("No categories found.");
            }
            return Ok(categoriesDto);
        }

        [HttpGet]
        [Route("get/content/{id}")]
        public async Task<IActionResult> GetCategoryContent(int id)
        {
            var category = await _service.GetCategoryContent(id);
            if (category == null)
            {
                return NotFound("No category with that ID.");
            }
            return Ok(category);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _service.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("No category with that ID");
            }
            return Ok(category);
        }
    }
}
