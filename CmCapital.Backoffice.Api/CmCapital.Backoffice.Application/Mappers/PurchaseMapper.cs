using CmCapital.Backoffice.Application.Model;
using CmCapital.Backoffice.Application.Queries.Response;
using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Application.Mappers;

public static class PurchaseMapper
{
    public static ListProductByClientResponse ToProductByClient(ProductsByClientModel model)
        => new()
        {
            Products = model
        };

    public static GetLessSoldProductResponse ToLessSoldProductResponse(Product lessSold)
        => new()
        {
            LessSoldProduct = lessSold,
        };

    public static GetMostSoldProductResponse ToMostSoldProductResponse(Product mostSold)
        => new()
        {
            MostSoldProduct = mostSold,
        };
}