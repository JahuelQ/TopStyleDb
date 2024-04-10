using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopStyleDb.Models.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public int? ParentCategoryId { get; set; }

        [ForeignKey("ParentCategoryId")]
        public Category ParentCategory { get; set;}

        public ICollection<Category> ChildCategories { get; set;} = new List<Category>();

        public ICollection<Product> Products { get; set;} = new List<Product>();


    }
}
