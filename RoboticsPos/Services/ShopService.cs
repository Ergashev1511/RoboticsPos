using System.Runtime.InteropServices.JavaScript;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class ShopService : IShopService
{
    private readonly IShopRepository _shopRepository;

    public ShopService(IShopRepository shopRepository)
    {
        _shopRepository = shopRepository;
    }
    
    public async Task CreateShop(ShopDTO shopDto)
    {
        Shop shop = new Shop();
        shop.TotalSum = shopDto.TotalSum;
        shop.TotalPaySum = shopDto.TotalPaySum;
        shop.ClientId = shopDto.ClientId;
        shop.Payments = shopDto.Payments.Select(a => new Payment()
        {
            Sum = a.Sum,
            PayedSum = a.PayedSum,
            RemainingSum = a.RemainingSum,
            PaymentStatus = a.PaymentStatus,
            PaymentType = a.PaymentType,
            DebtId = a.DebtId,
        }).ToList();
        shop.Carts = shopDto.Carts.Select(a => new Cart()
        {
            Count = a.Count,
            TotalSum = a.TotalSum,
            ProductId = a.ProductId,
            ActualPrice = a.ActualPrice,
            SalePrice = a.SalePrice,
            DiscountId = a.DiscountId,
        }).ToList();

        await _shopRepository.AddAsync(shop);
    }

    public async Task<List<ShopHistoryForTable>> GetAllShops()
    {
        return await _shopRepository.GetAllShop();
    }

    
}

public interface IShopService
{
    Task CreateShop(ShopDTO shopDto);
    Task<List<ShopHistoryForTable>> GetAllShops();

}