using RoboticsPos.Data.Base;
using RoboticsPos.Data.Enum;

namespace RoboticsPos.Data.Models;

public class Debt : BaseEntity
{
    public decimal DebtSum { get; set; }
    public DebtStatus DebtStatus { get; set; }

    public long  ClientId { get; set; }
    public virtual Client Client { get; set; }

    public long  PaymentId { get; set; }
    public virtual Payment  Payment { get; set; }

    public virtual List<DebtPayment> DebtPayments { get; set; }
}