using RoboticsPos.Data.Base;

namespace RoboticsPos.Data.Models;

public class ProductCategory : BaseEntity
{
    public string Name { get; set; }
    public string Describtion { get; set; }
  //  public virtual List<Product> Products { get; set; }
}