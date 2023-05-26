using CmCapital.Backoffice.Domain.Entities;
using System.Linq.Expressions;

namespace CmCapital.Backoffice.Infrastructure.Interfaces.Repository;

public interface IProductRepository
{
    Task<bool> AddAsync(Product product);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<IEnumerable<Product>> ListProductAsync(
        Expression<Func<Product, bool>> predicate);
    Task<Product> GetProductAsync(Guid id);
    Task<Product> UpdateAsync(Product product);
    Task<bool> DeleteAsync(Guid id);
    Task<int> CountAsync();
}