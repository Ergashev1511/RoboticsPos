using Microsoft.EntityFrameworkCore;
using RoboticsPos.Data.Models;

namespace RoboticsPos.Data.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _context;

    public CompanyRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Company> CreateCompany(Company company)
    {
        var hascopy =  _context.Company.Any(a => a.Name != company.Name);
        if (hascopy)
        {
            await _context.Company.AddAsync(company);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Current Company already exist!");
        }

        return company;
    }

    public async Task<Company> UpdateCompany(Company company)
    {
        _context.Company.Update(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<Company> GetByIdCompany(long Id)
    {
        if (Id < 0) throw new Exception("Id is low from 0!");
        var company = await _context.Company.FirstOrDefaultAsync(a => a.Id == Id);
        return company;
    }

    public async Task DeleteCompany(Company company)
    {
        company.IsDeleted = true;
        _context.Company.Update(company);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Company>> GetAll()
    {
        return await _context.Company.Where(a => !a.IsDeleted).ToListAsync();
    }
}

public interface ICompanyRepository
{
    Task<Company> CreateCompany(Company company);
    Task<Company> UpdateCompany(Company company);
    Task<Company> GetByIdCompany(long Id);
    Task DeleteCompany(Company company);
    Task<List<Company>> GetAll();
} 