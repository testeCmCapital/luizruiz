using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Commands.Request;
using CmCapital.Backoffice.Application.Commands.Response;
using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Interfaces.Services;

namespace CmCapital.Backoffice.Application.Commands.Handler;

public sealed class BuyProductHandler : ICommandHandler<BuyProductRequest, BuyProductResponse>
{
    private readonly IProductService _productService;
    private readonly IClientService _clientService;
    private readonly IPurchaseService _purchaseService;

    public BuyProductHandler(
        IProductService productService,
        IClientService clientService,
        IPurchaseService purchaseService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        _purchaseService = purchaseService ?? throw new ArgumentNullException(nameof(purchaseService));
    }

    public async Task<Result<BuyProductResponse?>> Handle(BuyProductRequest request, CancellationToken cancellationToken)
    {
        if (request.Quantity <= 0)
            return Result<BuyProductResponse?>.BadRequest("A quantidade precisa ser maior que 0");

        var count = await _productService.CountProductsAsync();
        if (!(count > 0))
            return Result<BuyProductResponse?>.NotFound("Acabaram os produtos, Deseja receber informações futuras, sobre produtos ? clique no link a baixo");

        var client = await _clientService.GetClientAsync(request.ClientId);
        if (client is null)
            return Result<BuyProductResponse?>.NotFound($"O Client: {request.ClientId} Não foi encontrado");

        var product = await _productService.GetProductAsync(request.ProductId);
        if (product is null)
            return Result<BuyProductResponse?>.NotFound($"O Produto: {request.ProductId} Não foi encontrado");

        var result = await _purchaseService.BuyProductAsync(client!, product!, request.Quantity);

        return Result<BuyProductResponse?>.Success();
    }
}