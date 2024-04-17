using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class CheckPrinterService : ICheckPrinterService
{
    private readonly ICheckPrinterRepository _repository;

    public CheckPrinterService(ICheckPrinterRepository repository)
    {
        _repository = repository;
    }

    public async Task<CheckPrinterDataDTO> Create(CheckPrinterDataDTO checkPrinterDataDto)
    {
        if (checkPrinterDataDto == null) throw new Exception("CheckPrinter is null");
        CheckPrintingData checkPrintingData = new CheckPrintingData()
        {
            Header = checkPrinterDataDto.Header,
            Footer = checkPrinterDataDto.Footer,
            Tara = checkPrinterDataDto.Tara,
            TIN = checkPrinterDataDto.TIN,
            Printer = checkPrinterDataDto.Printer
        };
        await _repository.CreateCheckprinter(checkPrintingData);
        return checkPrinterDataDto;
    }

    public async Task<CheckPrinterDataDTO> Update(long Id, CheckPrinterDataDTO checkPrinterDataDto)
    {
        var checkprinter = await _repository.GetCheckPrinterById(Id);
        if (checkprinter == null) throw new Exception("CheckPrinter is null here!");
        
        checkprinter.Footer = checkPrinterDataDto.Footer;
        checkprinter.Header = checkPrinterDataDto.Header;
        checkprinter.Tara = checkPrinterDataDto.Tara;
        checkprinter.TIN = checkPrinterDataDto.TIN;
        checkprinter.Printer = checkPrinterDataDto.Printer;

        await _repository.UpdateCheckPrinter(checkprinter);
        return checkPrinterDataDto;
    }

    public async Task<CheckPrinterDataDTO> GetById(long Id)
    {
        var checkprinter = await _repository.GetCheckPrinterById(Id);
        if (checkprinter == null) throw new Exception("CheckPrinter is null here!");

        CheckPrinterDataDTO checkPrinterDataDto = new CheckPrinterDataDTO()
        {
            Header = checkprinter.Header,
            Footer = checkprinter.Footer,
            Tara = checkprinter.Tara,
            Printer = checkprinter.Printer,
            TIN = checkprinter.TIN
        };
        return checkPrinterDataDto;

    }

    public async Task Delete(long Id)
    {
        var checkprinter = await _repository.GetCheckPrinterById(Id);
        if (checkprinter == null) throw new Exception("CheckPrinter is null here!");
        await _repository.DeleteCheckPrinter(checkprinter);
    }

    public async Task<List<CheckPrinterDataDTO>> GetAll()
    {
        var checkprinter = await _repository.GetAllCheckPrinter();
        if (checkprinter.Any())
        {
            var checkprint = checkprinter.Select(a => new CheckPrinterDataDTO()
            {
                Id = a.Id,
                Header = a.Header,
                Footer = a.Footer,
                Tara = a.Tara,
                Printer = a.Printer,
                TIN = a.TIN
            }).ToList();
            return checkprint;
        }

        return new List<CheckPrinterDataDTO>();
    }
}


public interface ICheckPrinterService
{
    Task<CheckPrinterDataDTO> Create(CheckPrinterDataDTO checkPrinterDataDto);
    Task<CheckPrinterDataDTO> Update(long Id, CheckPrinterDataDTO checkPrinterDataDto);
    Task<CheckPrinterDataDTO> GetById(long Id);
    Task Delete(long Id);
    Task<List<CheckPrinterDataDTO>> GetAll();
}

