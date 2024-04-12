using Microsoft.EntityFrameworkCore;
using RoboticsPos.Data.Models;

namespace RoboticsPos.Data.Repositories;

public class CheckPrinterRepository : ICheckPrinterRepository
{
    private readonly AppDbContext _context;

    public CheckPrinterRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<CheckPrintingData> CreateCheckprinter(CheckPrintingData checkPrintingData)
    {
        var checkprinter = await _context.CheckPrintingData.AnyAsync(a => !a.IsDeleted);
        if (checkprinter == null) throw new Exception("Current checkprinter already exist");
        await _context.CheckPrintingData.AddAsync(checkPrintingData);
        await _context.SaveChangesAsync();
        return checkPrintingData;
    }

    public async Task<CheckPrintingData> GetCheckPrinterById(long Id)
    {
        if (Id < 0) throw new Exception("Id is from low 0!");
        return await _context.CheckPrintingData.FirstOrDefaultAsync(a => a.Id == Id);
    }

    public async Task<CheckPrintingData> UpdateCheckPrinter(CheckPrintingData checkPrintingData)
    {
        _context.CheckPrintingData.Update(checkPrintingData);
       await _context.SaveChangesAsync();
        return  checkPrintingData;
    }

    public async Task<List<CheckPrintingData>> GetAllCheckPrinter()
    {
        return await _context.CheckPrintingData.Where(a => !a.IsDeleted).ToListAsync();
    }

    public async Task DeleteCheckPrinter(CheckPrintingData checkPrintingData)
    {
        checkPrintingData.IsDeleted = true;
        _context.CheckPrintingData.Update(checkPrintingData);
        await _context.SaveChangesAsync();
    }
}



public interface ICheckPrinterRepository
{
    Task<CheckPrintingData> CreateCheckprinter(CheckPrintingData checkPrintingData);
    Task<CheckPrintingData> GetCheckPrinterById(long Id);
    Task<CheckPrintingData> UpdateCheckPrinter(CheckPrintingData checkPrintingData);
    Task<List<CheckPrintingData>> GetAllCheckPrinter();
    Task DeleteCheckPrinter(CheckPrintingData checkPrintingData);
}