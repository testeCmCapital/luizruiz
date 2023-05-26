using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Infrastructure.Interfaces.Repository;

public interface IPurchaseRepository 
{
    Task<bool> AddAsync(Purchase purchase);
    Task<bool> DeleteAsync(Guid id);
    Task<Purchase> GetPurchaseAsync(Guid purchaseId);
    Task<IEnumerable<Purchase>> GetPurchasesByClientAsync(Guid clientId);
    Task<IEnumerable<Purchase>> GetPurchasesAsync();
}