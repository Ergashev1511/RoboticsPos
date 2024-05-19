using RoboticsPos.Data.Enum;

namespace RoboticsPos.Common.DTOs;

public class DebtDTO : BaseDTO
{
    public decimal DebtSum { get; set; }
    public DebtStatus DebtStatus { get; set; }
    
    public long ClientId { get; set; }
    public ClientDTO Client { get; set; }

    public long PaymentId { get; set; }
    public PaymentDTO Payment { get; set; }

    public List<DebtPaymentDTO>? DebtPayments { get; set; }
}