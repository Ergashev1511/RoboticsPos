namespace RoboticsPos.Common.DTOs;

public class ProductDTO : BaseDTO
{
    public string Name { get; set; }
    public string BarCode { get; set; }
    
    public int  AmountInPackage { get; set; }
   // public int CompanyId { get; set; }
    public int Amount { get; set; }
    public int ActualPrice { get; set; }
    public int Price { get; set; }
    public int PriceOfPiece { get; set; }
    public bool Selected { get; set; } = false;
    public long? DiscountId { get; set; }
}