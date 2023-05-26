using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Commands.Request;
using CmCapital.Backoffice.Application.Commands.Response;
using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Interfaces.Services;
using CmCapital.Backoffice.Application.Mappers;

namespace CmCapital.Backoffice.Application.Commands.Handler;

public sealed class CreateProductHandler : ICommandHandler<CreateProductRequest, CreateProductResponse>
{
    private readonly IProductService _productService;

    public CreateProductHandler(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    public async Task<Result<CreateProductResponse?>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _productService
            .CreateProductAsync(ProductMapper.ToProduct(request));

        if (result.IsSuccess)
            return Result<CreateProductResponse?>.Created(ProductMapper.ToCreateProductResponse(result!.Value!));

        return Result<CreateProductResponse?>.BadRequest(result!.Errors!.ToArray());
    }
}