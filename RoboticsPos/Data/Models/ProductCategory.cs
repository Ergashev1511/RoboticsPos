using RoboticsPos.Data.Base;

namespace RoboticsPos.Data.Models;

public class ProductCategory : BaseEntity
{
    public string Name { get; set; }
    public string Discription { get; set; }

    public long? ParentCategoryId { get; set; }
    public ProductCategory? ParentCategory { get; set; }

    public List<ProductCategory>? ChildCategories { get; set; }

    public virtual List<Product> Products { get; set; }
}