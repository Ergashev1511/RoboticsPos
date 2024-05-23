using Microsoft.EntityFrameworkCore;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Enum;
using RoboticsPos.Data.Models;

namespace RoboticsPos.Data.Repositories;

public class PaymentRepository :Repository<Payment>,IPaymentRepository
{
    public PaymentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<PaymentDTO>> GetAllPayments(long shopId)
    {
        var payments = await _dbContext.Shops.Where(a => a.Id == shopId).SelectMany(s => s.Payments).ToListAsync();

        var payment = payments.Select(c => new PaymentDTO()
        {
            Sum = c.Sum,
            PayedSum = c.PayedSum,
            RemainingSum = c.RemainingSum,
            PaymentType = c.PaymentType,
            PaymentStatus= c.PaymentStatus
        }).ToList();
        return payment;  // 
    }
}

public interface IPaymentRepository : IRepository<Payment>
{
    public Task<List<PaymentDTO>> GetAllPayments(long shopId);
}