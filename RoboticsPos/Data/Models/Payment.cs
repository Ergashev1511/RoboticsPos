using RoboticsPos.Data.Base;
using RoboticsPos.Data.Enum;

namespace RoboticsPos.Data.Models;

public class Payment : BaseEntity
{
    public decimal Sum { get; set; }
    public decimal PayedSum { get; set; }
    public decimal RemainingSum { get; set; }


    public PaymentType PaymentType { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public long?  DebtId { get; set; }
    public virtual Debt Debt { get; set; }

    public long? shopId { get; set; }
    public virtual Shop? Shop  { get; set; }

    public long? DebtPaymentId { get; set; }
    public virtual DebtPayment DebtPayment { get; set; }
}