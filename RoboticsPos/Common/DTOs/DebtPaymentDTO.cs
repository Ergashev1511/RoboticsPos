namespace RoboticsPos.Common.DTOs;

public class DebtPaymentDTO : BaseDTO
{
    public long DebtId { get; set; }
    public virtual DebtDTO Debt { get; set; }

    public long PaymentId { get; set; }
    public virtual PaymentDTO Payment { get; set; }
}