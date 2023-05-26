using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Commands.Request;
using CmCapital.Backoffice.Application.Commands.Response;
using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Interfaces.Services;
using CmCapital.Backoffice.Application.Mappers;

namespace CmCapital.Backoffice.Application.Commands.Handler;

public sealed class CreateClientHandler : ICommandHandler<CreateClientRequest, CreateClientResponse>
{
    private readonly IClientService _clientService;

    public CreateClientHandler(IClientService clientService)
    {
        _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
    }

    public async Task<Result<CreateClientResponse?>> Handle(CreateClientRequest request, CancellationToken cancellationToken)
    {
        var result = await _clientService
            .CreateClientAsync(ClientMapper.ToClient(request));

        if (result.IsSuccess)
            return Result<CreateClientResponse?>.Created(ClientMapper.ToCreateClientResponse(result!.Value!));

        return Result<CreateClientResponse?>.BadRequest(result!.Errors!.ToArray());
    }
}