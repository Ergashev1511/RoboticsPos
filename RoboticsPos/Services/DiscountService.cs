using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Enum;
using RoboticsPos.Data.Models;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductRepository _productRepository;

    public DiscountService(IDiscountRepository discountRepository,IProductRepository productRepository)
    {
        _discountRepository = discountRepository;
        _productRepository = productRepository;
    }

    public async Task<List<Select>> GetSelectDiscount()
    {
        var discount = await _discountRepository.GetSelectForDiscount();
        if (discount != null)
        {
            return discount;
        }

        return new List<Select>();
    }

    public async Task<DiscountDTO> CreateDiscount(DiscountDTO discountDto)
    {
        if (discountDto != null)
        {
           var products = await _productRepository.GetProductsByIds(discountDto.ProductDtos.Select(s=>s.Id).ToList());
                
            Discount newDiscount = new Discount()
            {
                Title = discountDto.Title,
                Description = discountDto.Description,
                Amount = discountDto.Amount,
                AmountType = discountDto.AmountType,
                DiscountStatus = discountDto.DiscountStatus,
                StartDate = discountDto.StartDate,
                EndDate = discountDto.EndDate,
                Products = products
            };
            await _discountRepository.CreateDiscount(newDiscount);
            return discountDto;
        }
        return discountDto;
        
    }

    public async Task<DiscountDTO> UpdateDiscount(long Id, DiscountDTO discountDto)
    {
        if (Id > 0)
        {
            var oldDiscount = await _discountRepository.GetByIdDiscount(Id);
            if (oldDiscount != null)
            {
                var prodocts =
                    await _productRepository.GetProductsByIds(discountDto.ProductDtos.Select(a => a.Id).ToList());
               
                oldDiscount.Title = discountDto.Title;
                oldDiscount.Description = discountDto.Description;
                oldDiscount.Amount = discountDto.Amount;
                oldDiscount.AmountType = discountDto.AmountType;
                oldDiscount.DiscountStatus = discountDto.DiscountStatus;

                oldDiscount.StartDate = discountDto.StartDate;
                oldDiscount.EndDate = discountDto.EndDate;

                oldDiscount.Products = new List<Product>();
                oldDiscount.Products = prodocts;
        
                await _discountRepository.UpdateDiscount(oldDiscount);
                return discountDto;
            }
            else
            {
                throw new Exception("Discount not found!");
            }
        }
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
            EndDate = discount.EndDate,
            ProductDtos = discount.Products.Select(s=> new ProductForSelect()
            {
                Id = s.Id,
                Name = s.Name,
                Amount = s.Amount,
                Selected = true
            }).ToList()
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
                EndDate = a.EndDate,
                ProductNames = string.Join(", ",a.Products.Select(b=>b.Name)),
                ProductDtos = a.Products.Select(a=>new ProductForSelect()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Amount = a.Amount,
                    Selected = true
                }).ToList()
            }).ToList();
            return discount;
        }

        return new List<DiscountDTO>();
    }
}

public interface IDiscountService
{
    Task<List<Select>> GetSelectDiscount();
    Task<DiscountDTO> CreateDiscount(DiscountDTO discountDto);
    Task<DiscountDTO> UpdateDiscount(long Id, DiscountDTO discountDto);
    Task<DiscountDTO> GetByIdDiscount(long Id);
    Task DeleteDiscount(long Id);
    Task<List<DiscountDTO>> GetAllDiscount();
}