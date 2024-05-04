namespace RoboticsPos.Common.DTOs;

public class CompanyDTO : BaseDTO
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    
    public List<ProductForSelect> Products { get; set; }
    
    public string Producnames { get; set; }
    
}