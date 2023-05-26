using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Application.Interfaces.Services;

public interface IProductService
{
    Task<Result<Product?>> CreateProductAsync(Product product);
    Task<Result<IEnumerable<Product?>>> GetProductsAsync();
    Task<Result<Product?>> GetProductAsync(Guid id);
    Task<Result<Product?>> UpdateProductAsync(Product product);
    Task<Result<bool?>> DeleteProductAsync(Guid id);
    Task<Result<int?>> CountProductsAsync();
    Task<Result<IEnumerable<Product?>>> ListProductAsync(
        string category,
        float balance,
        float initialValue,
        DateTime expirationDate);
}