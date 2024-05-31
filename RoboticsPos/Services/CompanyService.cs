using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IProductRepository _productRepository;

    public CompanyService(ICompanyRepository companyRepository,IProductRepository productRepository)
    {
        _companyRepository = companyRepository;
        _productRepository = productRepository;
    }

    public async Task<List<Select>> GetSelectCompany()
    {
        var company = await _companyRepository.GetSelectForCompany();
        if (company != null)
        {
            return company;
        }

        return new List<Select>();
    }

    public async Task<CompanyDTO> CreateCompant(CompanyDTO companyDto)
    {
        if (companyDto != null)
        {
            var products = await _productRepository.GetProductsByIds(companyDto.Products.Select(a => a.Id).ToList());
            Company company = new Company()
            {
                Name = companyDto.Name,
                PhoneNumber = companyDto.PhoneNumber,
                Products = products
            };
            await _companyRepository.CreateCompany(company);
            return companyDto;
        }
        return companyDto;
    }

    public async Task<CompanyDTO> UpdateCompany(long Id, CompanyDTO companyDto)
    {
        if (Id > 0)
        {
            var oldcompany = await _companyRepository.GetByIdCompany(Id);
            if (oldcompany != null)
            {
                var products =
                    await _productRepository.GetProductsByIds(companyDto.Products.Select(a => a.Id).ToList());
                
                oldcompany.Name = companyDto.Name;
                oldcompany.PhoneNumber = oldcompany.PhoneNumber;
                
                oldcompany.Products = new List<Product>();
                oldcompany.Products = products;
                
                await _companyRepository.UpdateCompany(oldcompany);
                return companyDto;
            }
            else
            {
                throw new Exception("Company not found!");
            }
        }

        return companyDto;
    }

    public async Task<List<CompanyDTO>> GetAllCompany()
    {
        var companys = await _companyRepository.GetAll();
        if (companys.Any())
        {
            return companys.Select(a => new CompanyDTO()
            {
                Id = a.Id,
                Name = a.Name,
                PhoneNumber = a.PhoneNumber,
                Products = a.Products.Select(s=>new ProductForSelect()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Selected = true
                }).ToList(),
                Producnames = string.Join(", ", a.Products.Select(s=>s.Name))
            }).ToList();
        }

        return new List<CompanyDTO>();
    }

    public async Task DeleteCompany(long Id)
    {
        if (Id>0)
        {
            var company = await _companyRepository.GetByIdCompany(Id);
            if (company == null) throw new Exception("Company is null here!");
            await _companyRepository.DeleteCompany(company);
        }
        else
        {
            throw new Exception("Id id low from 0!");
        }
    }

    public async Task<CompanyDTO> GetByIdCompany(long Id)
    {
        if (Id > 0)
        {
            var company = await _companyRepository.GetByIdCompany(Id);
            if (company != null)
            {
                return new CompanyDTO()
                {
                    Id = company.Id,
                    Name = company.Name,
                    PhoneNumber = company.PhoneNumber,
                    
                    Products = company.Products.Select(a=>new ProductForSelect()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Selected = true
                    }).ToList()
                };

            }
            else
            {
                throw new Exception("Company not found!");
            }
        }

        return null;
    }
}

public interface ICompanyService
{
    Task<List<Select>> GetSelectCompany();
      Task<CompanyDTO> CreateCompant(CompanyDTO companyDto);
    Task<CompanyDTO> UpdateCompany(long Id, CompanyDTO companyDto);
    Task<List<CompanyDTO>> GetAllCompany();
    Task DeleteCompany(long Id);
    Task<CompanyDTO> GetByIdCompany(long Id);
}