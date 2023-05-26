using CmCapital.Backoffice.Application.Core.Http;
using MediatR;

namespace CmCapital.Backoffice.Application.Abstractions.MediatR;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse?>>
    where TQuery : IQuery<TResponse>
{

}