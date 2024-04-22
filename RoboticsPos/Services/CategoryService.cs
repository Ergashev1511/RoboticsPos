using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    
    public async Task<ProuctCategoryDTO> CreateCategory(ProuctCategoryDTO prouctCategoryDto)
    {
        if (prouctCategoryDto == null) throw new Exception("ProductCategory is null here!");
        ProductCategory productCategory = new ProductCategory()
        {
            Name = prouctCategoryDto.Name,
            Discription = prouctCategoryDto.Discription
        };
        await _categoryRepository.CreateCategory(productCategory);
        return prouctCategoryDto;
    }

    public async Task<ProuctCategoryDTO> UpdateCategory(long Id, ProuctCategoryDTO prouctCategoryDto)
    {
        var category = await _categoryRepository.GetByIdCategory(Id);
        if (category == null) throw new Exception("ProductCategory is null here!");
        
        category.Name = prouctCategoryDto.Name;
        category.Discription = prouctCategoryDto.Discription;

        await _categoryRepository.UpdateCategory(category);
        return prouctCategoryDto;
    }

    public async Task Delete(long Id)
    {
        var category = await _categoryRepository.GetByIdCategory(Id);
        if (category == null) throw new Exception("ProductCategory is null here!");
        await _categoryRepository.DeleteCategory(category);

    }

    public async Task<ProuctCategoryDTO> GetByIdCategory(long Id)
    {
        var category = await _categoryRepository.GetByIdCategory(Id);
        if (category == null) throw new Exception("ProductCategory is null here!");

       ProuctCategoryDTO prouctCategoryDto=new ProuctCategoryDTO()
        {
            Id = category.Id,
            Name = category.Name,
            Discription = category.Discription
        };
        return prouctCategoryDto;
    }

    public async Task<List<ProuctCategoryDTO>> GetAllCategory()
    {
        var categorys = await _categoryRepository.GetAllCategory();
        if (categorys.Any())
        {
            var category = categorys.Select(a => new ProuctCategoryDTO()
            {
                Id = a.Id,
                Name = a.Name,
                Discription = a.Discription
            }).ToList();
            return category;
        }

        return new List<ProuctCategoryDTO>();
    }
}

public interface ICategoryService
{
    Task<ProuctCategoryDTO> CreateCategory(ProuctCategoryDTO prouctCategoryDto);
    Task<ProuctCategoryDTO> UpdateCategory(long Id, ProuctCategoryDTO prouctCategoryDto);
    Task Delete(long Id);
    Task<ProuctCategoryDTO> GetByIdCategory(long Id);
    Task<List<ProuctCategoryDTO>> GetAllCategory();
} 