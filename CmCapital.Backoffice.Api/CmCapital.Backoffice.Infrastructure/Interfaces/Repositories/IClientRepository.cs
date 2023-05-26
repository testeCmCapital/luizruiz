using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Infrastructure.Interfaces.Repository;

public interface IClientRepository 
{
    Task<bool> AddAsync(Client client);
    Task<IEnumerable<Client>> GetClientsAsync();
    Task<Client> GetClientAsync(Guid id);
    Task<Client> UpdateAsync(Client client);
    Task<bool> DeleteAsync(Guid id);
}