using CmCapital.Backoffice.Application.Commands.Request;
using CmCapital.Backoffice.Application.Commands.Response;
using CmCapital.Backoffice.Application.Extensions;
using CmCapital.Backoffice.Application.Queries.Response;
using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Application.Mappers;

public static class ProductMapper
{
    public static Product ToProduct(CreateProductRequest request)
        => new() 
        {
            Id = request.Id,
            Description = request.Description,
            ExpirationDate = request.ExpirationDate,
            RegistrationDate = request.RegistrationDate,
            UnitValue = request.UnitValue,
            Category = request.Category.ToCategory(),
        };

    public static CreateProductResponse ToCreateProductResponse(Product product)
        => new()
        {
            Id = product.Id,
            Description = product.Description,
            ExpirationDate = product.ExpirationDate,
            RegistrationDate = product.RegistrationDate,
            UnitValue = product.UnitValue
        };

    public static ListProductResponse ToListProduct(Product product)
        => new()
        {
            Id = product.Id,
            Description = product.Description,
            ExpirationDate = product.ExpirationDate,
            RegistrationDate = product.RegistrationDate,
            UnitValue = product.UnitValue,
            Category = product.Category
        };
}