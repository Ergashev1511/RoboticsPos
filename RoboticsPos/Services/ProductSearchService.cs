using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class ProductSearchService : IProductSearchService
{
    private readonly IProductRepository _repository;

    public ProductSearchService(IProductRepository repository)
    {
        this._repository = repository;
        
    }


    public Task<List<ProductDTO>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductSearchDTO>> GetAllSearchFOrProducts()
    {
        throw new NotImplementedException();
    }

    public Task<ProductDTO> CreateProduct(ProductDTO productDto)
    {
        throw new NotImplementedException();
    }

    public Task<ProductDTO> UpdateProduct(long Id, ProductDTO productDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProduct(long Id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductDTO> GetProductById(long Id)
    {
        throw new NotImplementedException();
    }
}
public interface IProductSearchService
     {
         public Task<List<ProductDTO>> GetAllProducts();
         public Task<List<ProductSearchDTO>> GetAllSearchFOrProducts();
         public Task<ProductDTO> CreateProduct(ProductDTO productDto);
         public Task<ProductDTO> UpdateProduct(long Id,ProductDTO productDto);
 
         public Task DeleteProduct(long Id);
 
         public Task<ProductDTO> GetProductById(long Id);
 
 
     }