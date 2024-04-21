namespace RoboticsPos.Common.DTOs;

public class ProductDTO : BaseDTO
{
    public string Name { get; set; }
    public string BarCode { get; set; }
    
    public decimal  AmountInPackage { get; set; }
   // public int CompanyId { get; set; }
    public decimal Amount { get; set; }
    public decimal ActualPrice { get; set; }
    public decimal Price { get; set; }
    public decimal PriceOfPiece { get; set; }
    public bool Selected { get; set; } = false;
    public long? DiscountId { get; set; }
}