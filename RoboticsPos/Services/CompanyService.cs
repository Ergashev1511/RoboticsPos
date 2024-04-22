using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    
    public async Task<CompanyDTO> CreateCompant(CompanyDTO companyDto)
    {
        if (companyDto == null) throw new Exception("Company is null here!");
        Company company = new Company()
        {
            Name = companyDto.Name,
            PhoneNumber = companyDto.PhoneNumber
        };
        await _companyRepository.CreateCompany(company);
        return companyDto;
    }

    public async Task<CompanyDTO> UpdateCompany(long Id, CompanyDTO companyDto)
    {
        var oldcompany = await _companyRepository.GetByIdCompany(Id);
        if (oldcompany == null) throw new Exception("Company is null here!");
        oldcompany.Name = companyDto.Name;
        oldcompany.PhoneNumber = oldcompany.PhoneNumber;
        await _companyRepository.UpdateCompany(oldcompany);
        return companyDto;
    }

    public async Task<List<CompanyDTO>> GetAllCompany()
    {
        var companys = await _companyRepository.GetAll();
        if (companys.Any())
        {
            var comany = companys.Select(a => new CompanyDTO()
            {
                Id = a.Id,
                Name = a.Name,
                PhoneNumber = a.PhoneNumber
            }).ToList();
            return comany;
        }

        return new List<CompanyDTO>();
    }

    public async Task DeleteCompany(long Id)
    {
        var company = await _companyRepository.GetByIdCompany(Id);
        if (company == null) throw new Exception("Company is null here!");
        await _companyRepository.DeleteCompany(company);
    }

    public async Task<CompanyDTO> GetByIdCompany(long Id)
    {
        var company = await _companyRepository.GetByIdCompany(Id);
        if (company == null) throw new Exception("Company is null here!");

        CompanyDTO companyDto = new CompanyDTO()
        {
            Name = company.Name,
            PhoneNumber = company.PhoneNumber
        };
        return companyDto; 
    }
}

public interface ICompanyService
{
    Task<CompanyDTO> CreateCompant(CompanyDTO companyDto);
    Task<CompanyDTO> UpdateCompany(long Id, CompanyDTO companyDto);
    Task<List<CompanyDTO>> GetAllCompany();
    Task DeleteCompany(long Id);
    Task<CompanyDTO> GetByIdCompany(long Id);
}