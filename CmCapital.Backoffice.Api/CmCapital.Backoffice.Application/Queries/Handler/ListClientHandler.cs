using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Interfaces.Services;
using CmCapital.Backoffice.Application.Mappers;
using CmCapital.Backoffice.Application.Queries.Request;
using CmCapital.Backoffice.Application.Queries.Response;

namespace CmCapital.Backoffice.Application.Queries.Handler;

public sealed class ListClientHandler : IQueryHandler<ListClientRequest, IEnumerable<ListClientResponse>>
{
    private readonly IClientService _clientService;

    public ListClientHandler(IClientService clientService)
    {
        _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
    }

    public async Task<Result<IEnumerable<ListClientResponse>?>> Handle(
        ListClientRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _clientService.GetClientsAsync();

        if (result.IsSuccess)
            return Result<IEnumerable<ListClientResponse>?>
               .Success(result!.Value!.ToList().Select(selector: ClientMapper.ToListClient));

        return Result<IEnumerable<ListClientResponse>?>.BadRequest();
    }
}