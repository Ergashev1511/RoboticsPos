using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;

namespace RoboticsPos.Data.Repositories;

public class DebtRepository : Repository<Debt> ,IDebtRepository
{
    public DebtRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}

public interface IDebtRepository : IRepository<Debt>
{
    
}