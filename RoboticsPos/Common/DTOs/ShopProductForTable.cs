namespace RoboticsPos.Common.DTOs;

public class ShopProductForTable : BaseDTO
{
    public string Name { get; set; }
    public string BarCode { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Amount { get; set; }
}