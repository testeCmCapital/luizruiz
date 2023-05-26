using CmCapital.Backoffice.Application.Commands.Request;
using CmCapital.Backoffice.Application.Commands.Response;
using CmCapital.Backoffice.Application.Queries.Response;
using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Application.Mappers;

public static class ClientMapper 
{
    public static Client ToClient(CreateClientRequest request)
        => new()
        {
            Id = request.Id,
            Name = request.Name,
            Balance = request.Balance,
            InitialValue = request.Balance,
            LastPurchase = request.LastPurchase
        };

    public static CreateClientResponse ToCreateClientResponse(Client client)
        => new()
        {
            Id = client.Id,
            Name = client.Name,
            Balance = client.Balance,
            InitialValue = client.InitialValue,
            LastPurchase = client.LastPurchase
        };

    public static ListClientResponse ToListClient(Client client)
        => new()
        {
            Id = client.Id,
            Name = client.Name,
            Balance = client.Balance,
            InitialValue = client.InitialValue,
            LastPurchase = client.LastPurchase
        };
}
