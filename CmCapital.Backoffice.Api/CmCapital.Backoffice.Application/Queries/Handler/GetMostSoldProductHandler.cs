using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Interfaces.Services;
using CmCapital.Backoffice.Application.Mappers;
using CmCapital.Backoffice.Application.Queries.Request;
using CmCapital.Backoffice.Application.Queries.Response;

namespace CmCapital.Backoffice.Application.Queries.Handler;

public sealed class GetMostSoldProductHandler : IQueryHandler<GetMostSoldProductRequest, GetMostSoldProductResponse>
{
    private readonly IPurchaseService _purchaseService;

    public GetMostSoldProductHandler(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService ?? throw new ArgumentNullException(nameof(purchaseService));
    }

    public async Task<Result<GetMostSoldProductResponse?>> Handle(
        GetMostSoldProductRequest request,
        CancellationToken cancellationToken)
    {
        var resultMostSold = await _purchaseService.MostSoldProductAsync();
        if (!resultMostSold.IsSuccess)
            return Result<GetMostSoldProductResponse?>.BadRequest();

        return Result<GetMostSoldProductResponse?>.Success(PurchaseMapper.ToMostSoldProductResponse(resultMostSold.Value!));
    }
}