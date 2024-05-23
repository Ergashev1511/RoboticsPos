using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class PaymentService : IPaymentService
{
    private IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }
    public async Task<List<PaymentDTO>> GetAllPayments(long shopId)
    {
        return await _paymentRepository.GetAllPayments(shopId);
    }
}

public interface IPaymentService
{
    public Task<List<PaymentDTO>> GetAllPayments(long shopId);
}