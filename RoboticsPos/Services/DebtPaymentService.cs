using System.Collections.Generic;
using System.Threading.Tasks;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class DebtPaymentService : IDebtPaymentSevice
{
    private readonly IDebtPaymentRepository _debtPaymentRepository;


    public DebtPaymentService(IDebtPaymentRepository debtPaymentRepository)
    {
        _debtPaymentRepository = debtPaymentRepository;
    }
    public async Task<DebtPaymentDTO> CreateDebtPayment(DebtPaymentDTO debtPaymentDto)
    {
        DebtPayment debtPayment = new DebtPayment()
        {
            DebtId = debtPaymentDto.DebtId,
            Payment = new Payment()
            {
                PaymentType = debtPaymentDto.Payment.PaymentType,
                PaymentStatus = debtPaymentDto.Payment.PaymentStatus,
                PayedSum = debtPaymentDto.Payment.PayedSum,
                RemainingSum = debtPaymentDto.Payment.RemainingSum,
                Sum = debtPaymentDto.Payment.Sum,
            }
        };
        await _debtPaymentRepository.AddAsync(debtPayment);
        return debtPaymentDto;
    }

    public async Task<List<DebtPaymentForTable>> GetAllDebtPayments(long shopId)
    {
        return await _debtPaymentRepository.GetAllDebtPayments(shopId);
    }
}

public interface IDebtPaymentSevice
{
    public Task<DebtPaymentDTO> CreateDebtPayment(DebtPaymentDTO debtPaymentDto);
    public Task<List<DebtPaymentForTable>> GetAllDebtPayments(long shopId);
}