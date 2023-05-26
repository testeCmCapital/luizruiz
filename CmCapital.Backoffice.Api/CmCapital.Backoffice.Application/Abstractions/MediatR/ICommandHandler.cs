using CmCapital.Backoffice.Application.Core.Http;
using MediatR;

namespace CmCapital.Backoffice.Application.Abstractions.MediatR;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse?>>
    where TCommand : ICommand<TResponse>
{

}