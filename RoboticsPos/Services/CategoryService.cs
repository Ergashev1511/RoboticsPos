using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private IProductRepository _productRepository { get; set; }

    public CategoryService(ICategoryRepository categoryRepository,IProductRepository productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }


    public async Task<List<Select>> GetCategoriesForSelect(long? parentId=null)
    {
        return await _categoryRepository.GetCategoriesForSelect(parentId);
    }

    public async Task<ProuctCategoryDTO> CreateCategory(ProuctCategoryDTO prouctCategoryDto)
    {
        List<Product> productsId = new List<Product>();
        if (prouctCategoryDto != null)
        {
            productsId = await _productRepository.GetProductsByIds(prouctCategoryDto.ProductDtos.Select(s=>s.Id).ToList());
            ProductCategory category = new ProductCategory()
            {
                Name = prouctCategoryDto.Name,
                Discription = prouctCategoryDto.Discription,
                ParentCategoryId =  prouctCategoryDto.ParentId,
                Products = productsId
                
            };
            await _categoryRepository.CreateCategory(category);
            return prouctCategoryDto;
        }

        return prouctCategoryDto;
    }

    public async Task<ProuctCategoryDTO> UpdateCategory(long Id, ProuctCategoryDTO prouctCategoryDto)
    {
        if (Id > 0)
        {
            var oldcategory = await _categoryRepository.GetByIdCategory(Id);
            if (oldcategory != null)
            {
                var products =
                    await _productRepository.GetProductsByIds(prouctCategoryDto.ProductDtos.Select(s => s.Id).ToList());
                oldcategory.Name = prouctCategoryDto.Name;
                oldcategory.Discription = prouctCategoryDto.Discription;
                oldcategory.ParentCategoryId = prouctCategoryDto.ParentId;

                oldcategory.Products = new List<Product>();
                oldcategory.Products = products;

                await _categoryRepository.UpdateCategory(oldcategory);
                return prouctCategoryDto;

            }
            else
            {
                MessageBox.Show("Id is low from 0!");
            }
        }

        return prouctCategoryDto;

    }

    public async Task Delete(long Id)
    {
        if (Id > 0)
        {
             var category = await _categoryRepository.GetByIdCategory(Id);
                    if (category == null) throw new Exception("ProductCategory is null here!");
                    await _categoryRepository.DeleteCategory(category);
        }
        else
        {
            MessageBox.Show("Id is low from 0!");
        }
       

    }

    public async Task<ProuctCategoryDTO> GetByIdCategory(long Id)
    {
        var category = await _categoryRepository.GetByIdCategory(Id);
        if (category != null)
        {
            return new ProuctCategoryDTO()
            {
                Id = category.Id,
                Name = category.Name,
                Discription = category.Discription,
                ParentName = category?.ParentCategory?.Name ?? string.Empty,
                ProductDtos = category.Products.Select(a => new ProductForSelect()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Amount = a.Amount,
                    Selected = true
                }).ToList()
            };
        }
        else
        {
            throw new Exception("Category not found!");
        }
    }

    public async Task<List<ProuctCategoryDTO>> GetAllCategory()
    {
        var categories = await _categoryRepository.GetAllCategory();
        if (categories.Any())
        {
            return categories.Select(s => new ProuctCategoryDTO()
            {
                Id = s.Id,
                Name = s.Name,
                Discription = s.Discription,
                ParentName = s?.ParentCategory?.Name ?? string.Empty,
                ProductDtos = s.Products.Select(a=>new ProductForSelect()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Amount = a.Amount,
                    Selected = true
                }).ToList(),
                Productnames = string.Join(", ", s.Products.Select(s=>s.Name))

            }).ToList();
        }

        return new List<ProuctCategoryDTO>();
    }

    public async Task<bool> HasChildCategory(long categoryId)
    {
        return await _categoryRepository.HasChildCategory(categoryId);
    }
}

public interface ICategoryService
{
    Task<List<Select>> GetCategoriesForSelect(long? parentId);
    Task<ProuctCategoryDTO> CreateCategory(ProuctCategoryDTO prouctCategoryDto);
    Task<ProuctCategoryDTO> UpdateCategory(long Id, ProuctCategoryDTO prouctCategoryDto);
    Task Delete(long Id);
    Task<ProuctCategoryDTO> GetByIdCategory(long Id);
    Task<List<ProuctCategoryDTO>> GetAllCategory();
    Task<bool> HasChildCategory(long categoryId);
} 