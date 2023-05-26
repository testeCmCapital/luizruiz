using CmCapital.Backoffice.Domain.Entities;
using CmCapital.Backoffice.Infrastructure.Interfaces.Repository;
using CmCapital.Backoffice.Infrastructure.Persistence.Context;
using CmCapital.Backoffice.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CmCapital.Backoffice.Infrastructure.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly DbContextOptions<CmCapitalContext> _dbContextOptions;

    public ProductRepository(
        DbContextOptions<CmCapitalContext> options)
    {
        _dbContextOptions = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<bool> AddAsync(Product product) 
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Product>();

        repository.Add(product);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync() 
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Product>();

        return await repository.GetAllAsync();
    }

    public async Task<Product> GetProductAsync(Guid id)
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Product>();

        return await repository.GetByIdAsync(p => p.Id == id);
    }

    public async Task<Product> UpdateAsync(Product product) 
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Product>();

        var productToUpdate = await repository.GetByIdAsync(p => p.Id == product.Id);
        if (productToUpdate is not null) 
        {
            repository.Update(product);
            await unitOfWork.SaveChangesAsync();
            return productToUpdate;
        }

        return product;
    }

    public async Task<bool> DeleteAsync(Guid id) 
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Product>();

        var productToDelete = await repository.GetByIdAsync(p => p.Id == id);
        if (productToDelete is not null) 
        {
            repository.Delete(productToDelete);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<int> CountAsync() 
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Product>();

        return await repository.CountAsync();
    }

    public async Task<IEnumerable<Product>> ListProductAsync(Expression<Func<Product, bool>> predicate)
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Product>();

        return await repository.ListAsync(predicate, x => x.Id);
    }
}