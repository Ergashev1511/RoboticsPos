namespace RoboticsPos.Common.DTOs;

public class ShopHistoryForTable : BaseDTO
{
    public decimal Totalsum { get; set; }
    public decimal Totalpaysum { get; set; }
    public string ClienName { get; set; }
    public string ShopCreateDate { get; set; }
}