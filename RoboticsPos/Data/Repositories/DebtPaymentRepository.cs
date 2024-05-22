using RoboticsPos.Common.DTOs;
using RoboticsPos.UI.Reports;
using DebtPayment = RoboticsPos.Data.Models.DebtPayment;

namespace RoboticsPos.Data.Repositories;

public class DebtPaymentRepository :Repository<DebtPayment> ,IDebtPaymentRepository
{
    public DebtPaymentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    
}

public interface IDebtPaymentRepository : IRepository<DebtPayment>
{
 
}