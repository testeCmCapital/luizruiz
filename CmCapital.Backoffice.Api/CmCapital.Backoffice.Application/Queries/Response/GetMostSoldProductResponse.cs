using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Application.Queries.Response;

public sealed class GetMostSoldProductResponse
{
    public Product MostSoldProduct { get; set; } = default!;
}