using RoboticsPos.Data.Base;

namespace RoboticsPos.Data.Models;

public class Shop : BaseEntity
{
    public decimal TotalSum { get; set; }

    public virtual List<Cart> Carts { get; set; }

    public long ClientId { get; set; }
    public virtual Client Client { get; set; }
}