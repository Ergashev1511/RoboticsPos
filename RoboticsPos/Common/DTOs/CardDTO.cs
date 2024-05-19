namespace RoboticsPos.Common.DTOs;

public class CardDTO : BaseDTO
{
    public decimal ActualPrice { get; set; }
    public decimal SalePrice { get; set; }
    public decimal Count { get; set; }
    public decimal TotalSum { get; set; }

    public long? DiscountId { get; set; }
    public DiscountDTO? Discount { get; set; }

    public long? ProductId { get; set; }
    public ProductDTO? Product { get; set; }

    public long? ShopId { get; set; }
    public ShopDTO? Shop { get; set; }
}