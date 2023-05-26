using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Application.Interfaces.Services;

public interface IClientService
{
    Task<Result<Client?>> CreateClientAsync(Client client);
    Task<Result<IEnumerable<Client?>>> GetClientsAsync();
    Task<Result<Client?>> GetClientAsync(Guid id);
    Task<Result<Client?>> UpdateClientAsync(Client client);
    Task<Result<bool?>> RollbackBalance(Guid clientId, float amount);
    Task<Result<bool?>> DeleteClientAsync(Guid id);
}