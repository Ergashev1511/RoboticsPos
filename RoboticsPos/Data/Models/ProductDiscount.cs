using RoboticsPos.Data.Base;

namespace RoboticsPos.Data.Models;

public class ProductDiscount : BaseEntity
{
    public long ProductId { get; set; }
    public long  DiscountId { get; set; }
    public Product Product { get; set; }
    public Discount Discount { get; set; }
}