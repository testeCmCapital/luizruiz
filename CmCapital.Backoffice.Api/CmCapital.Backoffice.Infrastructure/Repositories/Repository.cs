using CmCapital.Backoffice.Domain.Entities;
using CmCapital.Backoffice.Infrastructure.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CmCapital.Backoffice.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).FirstOrDefaultAsync() ?? default!;
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<List<T>> ListAsync(
        Expression<Func<T, bool>> predicate, 
        Expression<Func<T, object>> sortExpression)
    {
        return (await _dbSet.Where(predicate).OrderByDescending(sortExpression).ToListAsync());
    }
    public async Task<int> CountAsync() 
    {
        return await _dbSet.CountAsync();
    }
}