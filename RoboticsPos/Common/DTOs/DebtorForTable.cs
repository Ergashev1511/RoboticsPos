namespace RoboticsPos.Common.DTOs;

public class DebtorForTable : BaseDTO
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public decimal  DebtSum { get; set; }
    public string LastPaymentDate { get; set; }
}