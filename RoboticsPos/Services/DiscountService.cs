using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Enum;
using RoboticsPos.Data.Models;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;

    public DiscountService(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }
    
    public async Task<DiscountDTO> CreateDiscount(DiscountDTO discountDto)
    {
        if (discountDto == null) throw new Exception("Discount is null here!");

        Discount discount = new Discount()
        {
            Title = discountDto.Title,
            Description = discountDto.Description,
            Amount = discountDto.Amount,
            AmountType = discountDto.AmountType,
            DiscountStatus = discountDto.DiscountStatus,
            StartDate = discountDto.StartDate,
            EndDate = discountDto.EndDate
        };
        await _discountRepository.CreateDiscount(discount);
        return discountDto;
    }

    public async Task<DiscountDTO> UpdateDiscount(long Id, DiscountDTO discountDto)
    {
        var oldDiscount = await _discountRepository.GetByIdDiscount(Id);
        if (oldDiscount == null) throw new Exception("Discount is null here!");

        oldDiscount.Title = discountDto.Title;
        oldDiscount.Description = discountDto.Description;
        oldDiscount.Amount = discountDto.Amount;
        oldDiscount.AmountType = discountDto.AmountType;
        oldDiscount.DiscountStatus = discountDto.DiscountStatus;

        oldDiscount.StartDate = discountDto.StartDate;
        oldDiscount.EndDate = discountDto.EndDate;
        await _discountRepository.UpdateDiscount(oldDiscount);
        return discountDto;
    }

    public async Task<DiscountDTO> GetByIdDiscount(long Id)
    {
        var discount = await _discountRepository.GetByIdDiscount(Id);
        if (discount == null) throw new Exception("Discount is null here!");

        DiscountDTO discountDto = new DiscountDTO()
        {
            Title = discount.Title,
            Description = discount.Description,
            Amount = discount.Amount,
            AmountType = discount.AmountType,
            DiscountStatus = discount.DiscountStatus,
            StartDate = discount.StartDate,
            EndDate = discount.EndDate
        };
        return discountDto;
    }

    public async Task DeleteDiscount(long Id)
    {
        var discount = await _discountRepository.GetByIdDiscount(Id);
        if (discount == null) throw new Exception("Discount is null here!");

        await _discountRepository.DeleteDiscount(discount);
    }

    public async Task<List<DiscountDTO>> GetAllDiscount()
    {
        var discounts = await _discountRepository.GetAllDiscount();
        if (discounts.Any())
        {
            var discount = discounts.Select(a => new DiscountDTO()
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Amount = a.Amount,
                AmountType = a.AmountType,
                DiscountStatus = a.DiscountStatus,
                StartDate = a.StartDate,
                EndDate = a.EndDate
            }).ToList();
            return discount;
        }

        return new List<DiscountDTO>();
    }
}

public interface IDiscountService
{
    Task<DiscountDTO> CreateDiscount(DiscountDTO discountDto);
    Task<DiscountDTO> UpdateDiscount(long Id, DiscountDTO discountDto);
    Task<DiscountDTO> GetByIdDiscount(long Id);
    Task DeleteDiscount(long Id);
    Task<List<DiscountDTO>> GetAllDiscount();
}