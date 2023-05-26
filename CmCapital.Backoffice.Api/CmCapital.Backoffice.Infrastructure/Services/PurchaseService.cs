using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Interfaces.Services;
using CmCapital.Backoffice.Application.Model;
using CmCapital.Backoffice.Domain.Entities;
using CmCapital.Backoffice.Infrastructure.Interfaces.Repository;
using CmCapital.Backoffice.Infrastructure.Validators.Purchase;

namespace CmCapital.Backoffice.Infrastructure.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IClientService _clientService;
    private readonly IProductService _productService;

    public PurchaseService(
        IPurchaseRepository purchaseRepository,
        IClientService clientService,
        IProductService productService)
    {
        _purchaseRepository = purchaseRepository ?? throw new ArgumentNullException(nameof(purchaseRepository));
        _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    private async Task<Result<Purchase?>> CreatePurchaseAsync(Purchase purchase)
    {
        var created = await _purchaseRepository.AddAsync(purchase);

        return created
            ? Result<Purchase?>.Created(purchase)
            : Result<Purchase?>.BadRequest("Não foi possivel cadastrar o client");
    }

    public async Task<Result<PurchaseOrderModel?>> BuyProductAsync(Client client, Product product, int Quantity)
    {
        var lstProd = await _productService.ListProductAsync(
            product.Category,
            client.Balance,
            client.InitialValue,
            product.ExpirationDate);

        var validatorChain = new ResidualValidator();
        validatorChain.SetNextValidator(new BalanceValidator(lstProd!.Value!.ToList()));
 
        var validator = validatorChain.Validate(
            new() 
            { 
                Product = product, 
                Client = client, 
                Quantity = Quantity 
            });

        if (validator.StatusCode is not System.Net.HttpStatusCode.OK)
            return Result<PurchaseOrderModel?>
                .Conflict(
                new PurchaseOrderModel() 
                {
                    Products = validator.Value!.Products
                }, validator.Value!.Errors!.ToArray());

        //adicionar no banco
        var purchase = new Purchase() 
        {
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            ClientId = client.Id,
            Quantity = Quantity,
            Amount = product.UnitValue * Quantity,
            UnitValue = product.UnitValue,
            CreateAt = DateTime.UtcNow,
        };

        var created = await CreatePurchaseAsync(purchase);

        if(!created.IsSuccess)
            return Result<PurchaseOrderModel?>
                .InternalServerError();

        client.Balance -= (product.UnitValue * Quantity);
        client.LastPurchase = DateTime.Now;

        await _clientService.UpdateClientAsync(client);

        return Result<PurchaseOrderModel?>
            .Success(
            new PurchaseOrderModel 
            { 
                Product = product,
                Purchase = purchase,
            });
    }

    public async Task<Result<Purchase?>> GetPurchaseAsync(Guid purchaseId)
    {
        var purchase = await _purchaseRepository.GetPurchaseAsync(purchaseId);

        return purchase is not null 
            ? Result<Purchase?>.Success(purchase) 
            : Result<Purchase?>.NotFound();
    }

    public async Task<Result<IEnumerable<Purchase>?>> GetPurchasesAsync() 
    {
        var purchases = await _purchaseRepository.GetPurchasesAsync();

        return Result<IEnumerable<Purchase>>.Success(purchases)!;
    }

    public async Task<Result<IEnumerable<ProductsByClientModel?>>> GetProductsByClientAsync(Guid clientId)
    {
        var lstPurchase = await _purchaseRepository.GetPurchasesByClientAsync(clientId);
        if (!lstPurchase.Any())
            Result<IEnumerable<Purchase?>>.BadRequest();

        var purchases = lstPurchase
            .GroupBy(x => new { x.ClientId, x.ProductId })
            .Select(g => new ProductsByClientModel
            {
                ClientId = g.Key.ClientId,
                Quantity = g.Sum(pc => pc.Quantity),
                Amount = g.Sum(pc => pc.Amount),
                ProductId = g.Key.ProductId,
            });

        return Result<IEnumerable<ProductsByClientModel?>>.Success(purchases);
    }

    public async Task<Result<Product?>> MostSoldProductAsync()
    {
        var lstPurchase = await _purchaseRepository.GetPurchasesAsync();
        if (!lstPurchase.Any())
            return Result<Product?>.BadRequest();

        var purchaseGroup = lstPurchase
            .GroupBy(x => new { x.ProductId, x.Quantity })
            .Select(g => new
            {
                g.Key.ProductId,
                Quantity = g.Sum(pc => pc.Quantity)
            })
            .OrderByDescending(x => x.Quantity);

        var purchase = purchaseGroup.FirstOrDefault();

        var product = await _productService.GetProductAsync(purchase!.ProductId);
        if (!product.IsSuccess)
            return Result<Product?>.BadRequest();

        return Result<Product?>.Success(product.Value!);
    }

    public async Task<Result<Product?>> LessSoldProductAsync() 
    {
        var lstPurchase = await _purchaseRepository.GetPurchasesAsync();
        if (!lstPurchase.Any())
            return Result<Product?>.BadRequest();

        var purchaseGroup = lstPurchase
            .GroupBy(x => new { x.ProductId, x.Quantity })
            .Select(g => new
            {
                g.Key.ProductId,
                Quantity = g.Sum(pc => pc.Quantity)
            })
            .OrderBy(x => x.Quantity); ;

        var purchase = purchaseGroup.FirstOrDefault();

        var product = await _productService.GetProductAsync(purchase!.ProductId);
        if(!product.IsSuccess)
            return Result<Product?>.BadRequest();

        return Result<Product?>.Success(product.Value!);
    }

    public async Task<Result<bool?>> DeleteAsync(Guid purchaseId)
    {
        var deleted = await _purchaseRepository.DeleteAsync(purchaseId);

        return deleted
            ? Result<bool?>.Success() 
            : Result<bool?>.NotFound();
    }
}