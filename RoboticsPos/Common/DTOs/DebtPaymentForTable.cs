using RoboticsPos.Data.Enum;

namespace RoboticsPos.Common.DTOs;

public class DebtPaymentForTable : BaseDTO
{ 
 
     public decimal DebtSum { get; set; }
    public decimal PayedSum { get; set; }
    public decimal RemainingSum { get; set; }
    public string PayDebtDate { get; set; }
}