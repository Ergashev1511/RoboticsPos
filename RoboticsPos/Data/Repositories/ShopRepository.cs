using RoboticsPos.Data.Models;

namespace RoboticsPos.Data.Repositories;

public class ShopRepository : IShopRepository
{
    private readonly AppDbContext _context;

    public ShopRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Shop> CreateShop(Shop shop)
    {
        await _context.Shops.AddAsync(shop);
        await _context.SaveChangesAsync();
        return shop;
    }
}

public interface IShopRepository
{
    Task<Shop> CreateShop(Shop shop);
}
