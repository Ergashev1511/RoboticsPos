namespace RoboticsPos.Common.DTOs;

public class ProductForKassaDTO : BaseDTO
{
    public int Index { get; set; }
    public string Name { get; set; }
    public string BarCode { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }
}