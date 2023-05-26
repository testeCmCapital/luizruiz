using CmCapital.Backoffice.Domain.Entities;
using CmCapital.Backoffice.Infrastructure.Interfaces.Repository;
using CmCapital.Backoffice.Infrastructure.Persistence.Context;
using CmCapital.Backoffice.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;

namespace CmCapital.Backoffice.Infrastructure.Repositories;

public sealed class PurchaseRepository : IPurchaseRepository
{
    private readonly DbContextOptions<CmCapitalContext> _dbContextOptions;

    public PurchaseRepository(
        DbContextOptions<CmCapitalContext> options)
    {
        _dbContextOptions = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<bool> AddAsync(Purchase purchase)
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Purchase>();

        repository.Add(purchase);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Purchase>();

        var purchaseToDelete = await repository.GetByIdAsync(p => p.Id == id);
        if (purchaseToDelete is not null)
        {
            repository.Delete(purchaseToDelete);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<Purchase> GetPurchaseAsync(Guid purchaseId)
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Purchase>();

        return await repository.GetByIdAsync(p => p.Id == purchaseId);
    }

    public async Task<IEnumerable<Purchase>> GetPurchasesByClientAsync(Guid clientId)
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Purchase>();

        return await repository.ListAsync(p => p.ClientId == clientId, p => p.CreateAt);
    }

    public async Task<IEnumerable<Purchase>> GetPurchasesAsync()
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Purchase>();

        return await repository.GetAllAsync();
    }

    public async Task<Purchase> UpdateAsync(Purchase purchase)
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Purchase>();

        var purchaseToUpdate = await repository.GetByIdAsync(p => p.Id == purchase.Id);
        if (purchaseToUpdate is not null)
        {
            repository.Update(purchase);
            await unitOfWork.SaveChangesAsync();
            return purchaseToUpdate;
        }

        return purchase;
    }
}