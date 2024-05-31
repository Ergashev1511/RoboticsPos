using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task<List<DebtPaymentForTable>> GetAllDebtPayments(long debtorId)
    {

        var debtpayment = await _dbContext.Debts.Where(a => a.ClientId == debtorId).SelectMany(a => a.DebtPayments)
            .Select(a => a.Payment).AsQueryable().ToListAsync();
        var alldebtpayments = debtpayment.Select(a => new DebtPaymentForTable()
        {
            Id = a.Id,
            DebtSum = a.Sum,
            PayedSum = a.PayedSum,
            RemainingSum = a.RemainingSum,
            PayDebtDate = a.CreateDate.ToString()
        }).ToList();
        return alldebtpayments;
    }
}

public interface IDebtPaymentRepository : IRepository<DebtPayment>
{
    public Task<List<DebtPaymentForTable>> GetAllDebtPayments(long debtorId);
}