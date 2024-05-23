using Microsoft.EntityFrameworkCore;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;

namespace RoboticsPos.Data.Repositories;

public class ShopRepository : Repository<Shop>, IShopRepository
{
    public ShopRepository(AppDbContext context) : base(context)
    {
        
    }

    public async Task<List<ShopHistoryForTable>> GetAllShop()
    {
        var shops = await _dbContext.Shops.Include(f=>f.Client).Include(s => s.Carts).Include(d => d.Payments).ToListAsync();
        var shopdtos = shops.Select(a=> new ShopHistoryForTable()
        {
            Id = a.Id,
            Totalpaysum =a.Payments.Sum(a=>a.PayedSum),
            Totalsum = a.TotalSum,
            ClienName = a.ClientId==null ? "Client" : $"{a.Client.Person.FirstName} {a.Client.Person.LastName}",
             ShopCreateDate =a.CreateDate.ToString("dd-MM-yyyy")
        }).ToList();
        return shopdtos;
        // ikkita muammo bor 1.Payedsum ni olish kerak    2. qachon savdo qilinganini chiqarish kerak.
    }

  
}

public interface IShopRepository:IRepository<Shop>
{
    public Task<List<ShopHistoryForTable>> GetAllShop();
 
}
