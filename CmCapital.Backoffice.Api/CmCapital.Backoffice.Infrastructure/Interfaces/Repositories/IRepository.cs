using System.Linq.Expressions;

namespace CmCapital.Backoffice.Infrastructure.Interfaces.Repository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> ListAsync(
        Expression<Func<T, bool>> predicate, 
        Expression<Func<T, object>> sortExpression);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> CountAsync();
}