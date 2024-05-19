namespace RoboticsPos.Common.DTOs;

public class ShopDTO : BaseDTO
{
    public decimal TotalSum { get; set; }
    public decimal TotalPaySum { get; set; }

    public virtual List<CardDTO> Carts { get; set; }

    public long? ClientId { get; set; }
    public ClientDTO? Client { get; set; }

    public List<PaymentDTO>? Payments { get; set; }
}