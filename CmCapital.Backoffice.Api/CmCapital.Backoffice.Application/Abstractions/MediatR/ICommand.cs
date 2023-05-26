using CmCapital.Backoffice.Application.Core.Http;
using MediatR;

namespace CmCapital.Backoffice.Application.Abstractions.MediatR;

public interface ICommand<TResponse> : IRequest<Result<TResponse?>>, IBaseRequest
{
}