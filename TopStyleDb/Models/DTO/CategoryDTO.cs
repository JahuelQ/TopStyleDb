namespace TopStyleDb.Models.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ChildCategoryDTO> ChildCategories { get; set; }
    }

    public class ChildCategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class CreateCategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
