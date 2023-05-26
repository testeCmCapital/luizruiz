using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Application.Queries.Response;

public sealed class GetLessSoldProductResponse 
{
    public Product LessSoldProduct { get; set; } = default!;
}