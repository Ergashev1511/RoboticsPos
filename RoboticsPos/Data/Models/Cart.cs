using RoboticsPos.Data.Base;

namespace RoboticsPos.Data.Models;

public class Cart : BaseEntity
{
    public decimal ActualPrice { get; set; }
    public decimal SalePrice { get; set; }
    public decimal Count { get; set; }
    public decimal TotalSum { get; set; }

    public long DiscountId { get; set; }
    public Discount Discount { get; set; }

    public long ProductId { get; set; }
    public Product Product { get; set; }

    public long ShopId { get; set; }
    public virtual Shop Shop { get; set; }
}