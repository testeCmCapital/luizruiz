using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Interfaces.Services;
using CmCapital.Backoffice.Application.Mappers;
using CmCapital.Backoffice.Application.Queries.Request;
using CmCapital.Backoffice.Application.Queries.Response;

namespace CmCapital.Backoffice.Application.Queries.Handler;

public sealed class ListProductByClientHandler : IQueryHandler<ListProductByClientRequest, IEnumerable<ListProductByClientResponse>>
{
    private readonly IPurchaseService _purchaseService;

    public ListProductByClientHandler(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService ?? throw new ArgumentNullException(nameof(purchaseService));
    }

    public async Task<Result<IEnumerable<ListProductByClientResponse>?>> Handle(
        ListProductByClientRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _purchaseService.GetProductsByClientAsync(request.ClientId);

        if (!result.IsSuccess)
            return Result<IEnumerable<ListProductByClientResponse>?>.BadRequest();

        return Result<IEnumerable<ListProductByClientResponse>?>
            .Success(result.Value!.Select(PurchaseMapper.ToProductByClient));
    }
}