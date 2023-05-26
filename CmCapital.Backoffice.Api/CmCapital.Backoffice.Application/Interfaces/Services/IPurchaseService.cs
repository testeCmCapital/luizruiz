using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Model;
using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Application.Interfaces.Services;

public interface IPurchaseService 
{
    Task<Result<PurchaseOrderModel?>> BuyProductAsync(Client client, Product product, int Quantity);
    Task<Result<Purchase?>> GetPurchaseAsync(Guid purchaseId);
    Task<Result<IEnumerable<ProductsByClientModel?>>> GetProductsByClientAsync(Guid clientId);
    Task<Result<bool?>> DeleteAsync(Guid purchaseId);
    Task<Result<Product?>> LessSoldProductAsync();
    Task<Result<Product?>> MostSoldProductAsync();
    Task<Result<IEnumerable<Purchase>?>> GetPurchasesAsync();
}