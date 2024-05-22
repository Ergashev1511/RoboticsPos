using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class DebtorService
{
    private readonly IDebtorRepository _debtorRepository;

    public DebtorService(IDebtorRepository debtorRepository)
    {
        _debtorRepository = debtorRepository;
    }

  public async Task<List<DebtorForTable>> GetAllDebtors()
    {
        return await _debtorRepository.GetAllDebtors();
    }
}