using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Interfaces.Services;
using CmCapital.Backoffice.Application.Mappers;
using CmCapital.Backoffice.Application.Queries.Request;
using CmCapital.Backoffice.Application.Queries.Response;

namespace CmCapital.Backoffice.Application.Queries.Handler;

public sealed class ListProductsHandler : IQueryHandler<ListProductRequest, IEnumerable<ListProductResponse>>
{
    private readonly IProductService _productService;

    public ListProductsHandler(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    public async Task<Result<IEnumerable<ListProductResponse>?>> Handle(
        ListProductRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _productService.GetProductsAsync();

        if (result.IsSuccess)
            return Result<IEnumerable<ListProductResponse>?>
               .Success(result!.Value!.ToList().Select(selector: ProductMapper.ToListProduct));

        return Result<IEnumerable<ListProductResponse>?>.BadRequest();
    }
}