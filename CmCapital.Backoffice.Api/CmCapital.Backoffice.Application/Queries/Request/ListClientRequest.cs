using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Queries.Response;

namespace CmCapital.Backoffice.Application.Queries.Request;

public sealed class ListClientRequest : IQuery<IEnumerable<ListClientResponse>>
{
}