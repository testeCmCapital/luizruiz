using CmCapital.Backoffice.Application.Model;

namespace CmCapital.Backoffice.Application.Queries.Response;

public sealed class ListProductByClientResponse
{
    public ProductsByClientModel Products { get; set; }
}