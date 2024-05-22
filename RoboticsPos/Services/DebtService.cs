using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Enum;
using RoboticsPos.Data.Models;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class DebtService
{
    private readonly IDebtRepository _debtRepository;

    public DebtService(IDebtRepository debtRepository)
    {
        _debtRepository = debtRepository;
    }
    public async Task<DebtDTO> CreateDebt(DebtDTO debtDto)
    {
        
        Debt debt = new Debt();
        debt.ClientId = debtDto.ClientId;
        debt.DebtSum = debtDto.DebtSum;
        debt.DebtStatus = debtDto.DebtStatus;
        debt.Payment = new Payment()
        {
            PaymentType = PaymentType.Debt,
            Sum = debtDto.Payment.Sum,
            PayedSum = debtDto.Payment.PayedSum,
            PaymentStatus = PaymentStatus.Debted,
            RemainingSum = debtDto.Payment.RemainingSum,
        };

        await _debtRepository.AddAsync(debt);

        return debtDto;
    }
}