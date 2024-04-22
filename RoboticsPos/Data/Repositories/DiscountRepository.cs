using Microsoft.EntityFrameworkCore;
using RoboticsPos.Data.Models;

namespace RoboticsPos.Data.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly AppDbContext _context;

    public DiscountRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Discount> CreateDiscount(Discount discount)
    {
      // var hascopy= await _context.Discounts.Any(a=>a.)
      await _context.Discounts.AddAsync(discount);
      await _context.SaveChangesAsync();
      return discount;
    }

    public async Task<Discount> UpdateDiscount(Discount discount)
    {
         _context.Discounts.Update(discount);
        await _context.SaveChangesAsync();
        return discount;
    }

    public async Task<Discount> GetByIdDiscount(long Id)
    {
        if (Id < 0) throw new Exception("Id is low from 0!");
        var discount = await _context.Discounts.FirstOrDefaultAsync(a => !a.IsDeleted && a.Id == Id);
        return discount;
    }

    public async Task DeleteDiscount(Discount discount)
    {
        discount.IsDeleted = true;
        _context.Discounts.Update(discount);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Discount>> GetAllDiscount()
    {
        return await _context.Discounts.Where(a => !a.IsDeleted).ToListAsync();
        // include qilmadim List<Product> nullable bo'lgani uchun
    }
}

public interface IDiscountRepository
{
    Task<Discount> CreateDiscount(Discount discount);
    Task<Discount> UpdateDiscount(Discount discount);
    Task<Discount> GetByIdDiscount(long Id);
    Task DeleteDiscount(Discount discount);
    Task<List<Discount>> GetAllDiscount();
}