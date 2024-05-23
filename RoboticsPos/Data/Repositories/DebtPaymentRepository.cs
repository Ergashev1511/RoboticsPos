using Microsoft.EntityFrameworkCore;
using RoboticsPos.Common.DTOs;
using RoboticsPos.UI.Reports;
using DebtPayment = RoboticsPos.Data.Models.DebtPayment;

namespace RoboticsPos.Data.Repositories;

public class DebtPaymentRepository :Repository<DebtPayment> ,IDebtPaymentRepository
{
    public DebtPaymentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<DebtPaymentForTable>> GetAllDebtPayments(long shopId)
    {
        /*var debtpayments = await _dbContext.Payments.Where(a => a.shopId == shopId && a.DebtPayment!=null).Include(s => s.DebtPayment).ThenInclude(d=>d.Debt)
            .ToListAsync();*/
        var debtpaymen = await _dbContext.Debts.Where(a => a.Id == shopId && a.Payment.DebtPayment.Id != null)
            .Include(s => s.DebtPayments).ThenInclude(d => d.Payment).ToListAsync();
        /*var debtpaymen = await _dbContext.Debts.Where(a => a.Id == shopId && a.DebtPayments!=null).Include(s => s.Payment)
            .ThenInclude(d => d.DebtPayment).ToListAsync();*/

        var newdebtpayment = debtpaymen.Select(a => new DebtPaymentForTable()
        {
            Id = a.Payment.DebtPayment.Id,
            DebtSum = a.DebtSum,
            PayedSum = a.Payment.PayedSum,
            RemainingSum = a.Payment.RemainingSum,
            PayDebtDate = a.Payment.CreateDate.ToString()
        }).ToList();
        /*var debtpayment = debtpayments.Select(a => new DebtPaymentForTable()
        {
            Id = a.DebtPayment.Id,
            DebtSum = a.Debt.DebtSum,
            PayedSum = a.PayedSum,
            RemainingSum = a.RemainingSum,
            PayDebtDate = a.CreateDate.ToString() 
        }).ToList();*/
        return newdebtpayment;
    }
}

public interface IDebtPaymentRepository : IRepository<DebtPayment>
{
    public Task<List<DebtPaymentForTable>> GetAllDebtPayments(long debtId);
}