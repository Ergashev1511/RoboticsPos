using Microsoft.EntityFrameworkCore;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;

namespace RoboticsPos.Data.Repositories;

public class DebtorRepository : Repository<Debt> ,IDebtorRepository
{
    public DebtorRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

        public async Task<List<DebtorForTable>> GetAllDebtors()
        {
            var debtor = await _dbContext.Debts.Where(a => a.ClientId != null).Include(s => s.Payment)
                .Include(a => a.Client).ThenInclude(s => s.Person)
                .Include(a => a.Client).ThenInclude(s => s.Debts).ThenInclude(s => s.DebtPayments)
                .ThenInclude(d => d.Payment).ToListAsync();
            var result = debtor.DistinctBy(a => a.ClientId).Select(a => new DebtorForTable()
            {
                Id = a.Id,
                FullName = $"{a.Client.Person.LastName} {a.Client.Person.FirstName}",
                DebtSum =  a.Client.Debts.Sum(a=>a.DebtSum) - a.Client.Debts.Sum(d=>(decimal) d.DebtPayments.Select(f=>f.Payment).Sum(p=>p.PayedSum)),
                PhoneNumber = a.Client.Person.PhoneNumber,
                LastPaymentDate = a.CreateDate.ToString("dd-MM-yyyy")
            }).ToList();
            return result;
        }
}

public interface IDebtorRepository : IRepository<Debt>
{
    Task<List<DebtorForTable>> GetAllDebtors();
}