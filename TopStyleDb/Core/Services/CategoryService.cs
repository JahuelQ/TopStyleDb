using Microsoft.EntityFrameworkCore;
using TopStyleDb.Core.Interfaces;
using TopStyleDb.Data.Interfaces;
using TopStyleDb.Models.DTO;
using TopStyleDb.Models.Entities;

namespace TopStyleDb.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _repo;
        public CategoryService(ICategoryRepo repo)
        {
            _repo = repo;
        }


        public async Task<CreateCategoryDTO> CreateCategory(CreateCategoryDTO categoryDto)
        {
            var category = new Category
            {
                CategoryName = categoryDto.CategoryName,
                ParentCategoryId = categoryDto.ParentCategoryId
            };

            var createdCategory = await _repo.CreateCategory(category);
            if (createdCategory == null)
            {
                return null;
            }

            return new CreateCategoryDTO
            {
                CategoryId = createdCategory.CategoryId,
                CategoryName = createdCategory.CategoryName,
                ParentCategoryId = createdCategory.ParentCategoryId
            };
        }

        public async Task DeleteCategory(int categoryId)
        {
            await _repo.DeleteCategory(categoryId);
        }

        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            var categories = await _repo.GetAllCategories();
            return categories.Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                ChildCategories = c.ChildCategories.Select(cc => new ChildCategoryDTO
                {
                    CategoryId = cc.CategoryId,
                    CategoryName = cc.CategoryName
                }).ToList()
            }).ToList();
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            var category = await _repo.GetCategory(categoryId);
            if (category == null)
            {
                return null;
            }
            return new Category
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ChildCategories = category.ChildCategories.ToList()
            };
        }

        public async Task<CategoryContentDTO> GetCategoryContent(int categoryId)
        {
            var category = await _repo.GetCategory(categoryId);
            if (category == null)
            {
                return null;
            }

            return new CategoryContentDTO
            {
                CategoryName = category.CategoryName,
                Products = category.Products.Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId
                }).ToList()
            };
        }
    }
}
