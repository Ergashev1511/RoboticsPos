using System.Windows.Documents;
using Microsoft.EntityFrameworkCore;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;

namespace RoboticsPos.Data.Repositories;

public class CetegoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CetegoryRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<Select>> GetCategoriesForSelect()
    {
        var categories = await _context.ProductCategory.Where(a=>!a.IsDeleted).Select(s => new Select()
        {
            Id = s.Id,
            Name = s.Name
        }).AsSplitQuery().ToListAsync();
        
        return categories;
       
    }

    public async Task<ProductCategory> CreateCategory(ProductCategory productCategory)
    {
        await _context.ProductCategory.AddAsync(productCategory);
        await _context.SaveChangesAsync();
        return productCategory;
    }
   

    public async Task<ProductCategory> UpdateCategory(ProductCategory productCategory)
    {
        _context.ProductCategory.Update(productCategory);
        await _context.SaveChangesAsync();
        return productCategory;
    }

    public async Task<ProductCategory> GetByIdCategory(long Id)
    {
        if (Id < 0) throw new Exception("Id is low from 0!");
        return await _context.ProductCategory.FirstOrDefaultAsync(a => a.Id == Id);
    }

    

    public async Task DeleteCategory(ProductCategory productCategory)
    {
        productCategory.IsDeleted = true;
        _context.ProductCategory.Update(productCategory);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProductCategory>> GetAllCategory()
    {
        return await _context.ProductCategory.Where(a => !a.IsDeleted).Include(s=>s.Products).ToListAsync();
    }

    public async Task<bool> HasChildCategory(long categoryId)
    {
        var category = await _context.ProductCategory.Include(a => a.ChildCategories)
            .FirstOrDefaultAsync(s => s.Id == categoryId);
        if (category is not null)
        {
            if (category.ChildCategories != null && category.ChildCategories.Any())
                return true;
            else
             return  false;
        }
        else
        {
            throw new Exception("Category not found!");
        }
    }
}

public interface ICategoryRepository
{
    public Task<List<Select>> GetCategoriesForSelect();
    Task<ProductCategory> CreateCategory(ProductCategory productCategory);
    Task<ProductCategory> UpdateCategory(ProductCategory productCategory);
    Task<ProductCategory> GetByIdCategory(long Id);
    Task DeleteCategory(ProductCategory productCategory);
    Task<List<ProductCategory>> GetAllCategory();
    Task<bool> HasChildCategory(long categoryId);
}