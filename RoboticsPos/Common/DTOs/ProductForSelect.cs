namespace RoboticsPos.Common.DTOs;

public class ProductForSelect : BaseDTO
{
    public bool Selected { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
}