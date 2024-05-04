using System.Windows.Documents;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Enum;
using RoboticsPos.Data.Models;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        this._repository = repository;
        
    }


    public async Task<List<ProductForSelect>> GetProductsByIdsForDiscount(List<long> productIds)
    {
        var products = await _repository.GetAllProducts();
        if (products != null && products.Any())
        {
            return products.Select(a => new ProductForSelect ()
            {
                Id = a.Id,
                Name = a.Name,
                Amount = a.Amount,
                Selected = true
            }).ToList();
        }
        return new List<ProductForSelect>();
    }

    public async Task<List<ProductDTO>> GetAllProducts()
    {
        var products = await _repository.GetAllProducts();
        if (products.Any())
        {
            var product = products.Select(a =>  new ProductDTO()
            {
                Id = a.Id,
                Name = a.Name,
                Amount = a.Amount,
                AmountInPackage = a.AmountInPackage,
                BarCode = a.BarCode,
                Price = a.Price,
                PriceOfPiece = a.PriceOfPiece,
                Selected = a.Selected,
                DiscountId = a.DiscountId,
                ActualPrice = a.ActualPrice
            }).ToList();
            return product;
        }

        return new List<ProductDTO>();
    }

    public async Task<List<ProductSearchDTO>> GetAllSearchFOrProducts(string name)
    {
        return await _repository.ProductByName(name);
    }

    public async Task<ProductDTO> CreateProduct(ProductDTO productDto)
    {
        if (productDto == null) throw new Exception("Product is null!");
        Product products = new Product()
        {
         
            Name = productDto.Name,
            BarCode = productDto.BarCode,
            AmountInPackage = productDto.AmountInPackage,
            Amount = productDto.Amount,
            ActualPrice = productDto.ActualPrice,
            Price = productDto.Price,
            PriceOfPiece = productDto.PriceOfPiece,
            Selected = productDto.Selected,
            DiscountId = productDto.DiscountId,  
            CompanyId = productDto.CompanyId,
            CategoryId = productDto.CategoryId,
          
        };
        await _repository.CreateProduct(products);
        return productDto;
    }

    public async Task<ProductDTO> UpdateProduct(long Id, ProductDTO productDto)
    {
        var oldproduct = await _repository.GetByIdProduct(Id);
        if (oldproduct == null) throw new Exception("Product is null");
        
       
        oldproduct.Name = productDto.Name;
        oldproduct.BarCode = productDto.BarCode;
        oldproduct.Amount = productDto.Amount;
        oldproduct.AmountInPackage = productDto.AmountInPackage;
        oldproduct.ActualPrice = productDto.ActualPrice;
        oldproduct.Price = productDto.Price;
        oldproduct.PriceOfPiece = productDto.PriceOfPiece;
        oldproduct.Selected = productDto.Selected;
        oldproduct.DiscountId = productDto.DiscountId;
        await _repository.UpdateProduct(oldproduct);
        return productDto;
    }

    public async Task DeleteProduct(long Id)
    {
        var product = await _repository.GetByIdProduct(Id);
        if (product == null) throw new Exception("Product not found!");
        await _repository.DeleteProduct(Id);
    }

    public async Task<ProductDTO> GetProductById(long Id)
    {
        var products = await _repository.GetByIdProduct(Id);
        if (products == null) throw new Exception("Product is null!");

        ProductDTO productDto = new ProductDTO()
        {
           Id = products.Id,
           Name = products.Name,
           BarCode = products.BarCode,
           Amount = products.Amount,
           AmountInPackage = products.AmountInPackage,
           ActualPrice = products.ActualPrice,
           Price = products.Price,
           PriceOfPiece = products.PriceOfPiece,
           Selected = products.Selected,
           DiscountId = products.DiscountId
        };
        return productDto;
    }

    public async Task<List<ProductForSelect>> GetAllForSelect()
    {
        var products = await _repository.GetAllProducts();
        if (products != null && products.Any())
        {
            return products.Select(a => new ProductForSelect()
            {
                Id = a.Id,
                Name = a.Name,
               Amount = a.Amount,
               Selected = a.Selected
            }).ToList();
        }

        return new List<ProductForSelect>();
    }
}
public interface IProductService
     {
         public Task<List<ProductForSelect>> GetProductsByIdsForDiscount(List<long> productIds);
         public Task<List<ProductDTO>> GetAllProducts();
         public Task<List<ProductSearchDTO>> GetAllSearchFOrProducts(string name);
         public Task<ProductDTO> CreateProduct(ProductDTO productDto);
         public Task<ProductDTO> UpdateProduct(long Id, ProductDTO productDto);
 
         public Task DeleteProduct(long Id);
 
         public Task<ProductDTO> GetProductById(long Id);
         public Task<List<ProductForSelect>> GetAllForSelect();


     }