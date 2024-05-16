using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RoboticsPos.Data.Base;
using RoboticsPos.Data.Enum;

namespace RoboticsPos.Data.Models;

public class DebtPayment : BaseEntity
{
    public long DebtId { get; set; }
    public virtual Debt Debt { get; set; }


    public long  PaymentId { get; set; }
    public virtual Payment Payment { get; set; }
}