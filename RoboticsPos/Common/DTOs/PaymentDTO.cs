using RoboticsPos.Data.Enum;

namespace RoboticsPos.Common.DTOs;

public class PaymentDTO : BaseDTO
{
    public decimal Sum { get; set; }
    public decimal PayedSum { get; set; }
    public decimal RemainingSum { get; set; }

    public PaymentType PaymentType { get; set; }
    
    public PaymentStatus PaymentStatus { get; set; }
    
    public long? DebtId { get; set; }
    public DebtDTO Debt { get; set; }
    
    public long? ShopId { get; set; }
    public ShopDTO? Shop { get; set; }
    
    public long? DebtPaymentId { get; set; }
    public DebtPaymentDTO DebtPayment { get; set; }
}