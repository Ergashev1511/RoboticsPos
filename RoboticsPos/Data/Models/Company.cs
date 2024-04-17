using RoboticsPos.Data.Base;

namespace RoboticsPos.Data.Models;

public class Company : BaseEntity
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public virtual List<Product> Products { get; set; }
}