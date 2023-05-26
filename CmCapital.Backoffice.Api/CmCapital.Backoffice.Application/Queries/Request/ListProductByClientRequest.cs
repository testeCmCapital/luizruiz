using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Queries.Response;

namespace CmCapital.Backoffice.Application.Queries.Request;

public sealed class ListProductByClientRequest : IQuery<IEnumerable<ListProductByClientResponse>> 
{
    public ListProductByClientRequest(Guid clientId)
    {
        ClientId = clientId;
    }

    public Guid ClientId { get; set; }
}