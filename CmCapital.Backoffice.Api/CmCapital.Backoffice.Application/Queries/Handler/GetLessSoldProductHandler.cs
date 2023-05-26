using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Interfaces.Services;
using CmCapital.Backoffice.Application.Mappers;
using CmCapital.Backoffice.Application.Queries.Request;
using CmCapital.Backoffice.Application.Queries.Response;

namespace CmCapital.Backoffice.Application.Queries.Handler;

public sealed class GetLessSoldProductHandler : IQueryHandler<GetLessSoldProductRequest, GetLessSoldProductResponse>
{
    private readonly IPurchaseService _purchaseService;

    public GetLessSoldProductHandler(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService ?? throw new ArgumentNullException(nameof(purchaseService));
    }

    public async Task<Result<GetLessSoldProductResponse?>> Handle(
        GetLessSoldProductRequest request, 
        CancellationToken cancellationToken)
    {
        var resultMostSold = await _purchaseService.MostSoldProductAsync();
        if (!resultMostSold.IsSuccess)
            return Result<GetLessSoldProductResponse?>.BadRequest();

        var resultLessSold = await _purchaseService.LessSoldProductAsync();
        if(!resultLessSold.IsSuccess)
            return Result<GetLessSoldProductResponse?>.BadRequest();

        return Result<GetLessSoldProductResponse?>.Success(PurchaseMapper.ToLessSoldProductResponse(resultLessSold.Value!));
    }
}