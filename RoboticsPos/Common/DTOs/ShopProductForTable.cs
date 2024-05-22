namespace RoboticsPos.Common.DTOs;

public class ShopProductForTable : BaseDTO
{
    public string Name { get; set; }
    public string Barcode { get; set; }
    public decimal Amount { get; set; }
    public decimal TotalPrice { get; set; }
}