using CmCapital.Backoffice.Domain.Entities;
using CmCapital.Backoffice.Infrastructure.Interfaces.Repository;
using CmCapital.Backoffice.Infrastructure.Persistence.Context;
using CmCapital.Backoffice.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;

namespace CmCapital.Backoffice.Infrastructure.Repositories;

public sealed class ClientRepository : IClientRepository
{
    private readonly DbContextOptions<CmCapitalContext> _dbContextOptions;

    public ClientRepository(
        DbContextOptions<CmCapitalContext> options)
    {
        _dbContextOptions = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<bool> AddAsync(Client client)
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Client>();

        repository.Add(client);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Client>> GetClientsAsync()
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Client>();

        return await repository.GetAllAsync();
    }

    public async Task<Client> GetClientAsync(Guid id)
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Client>();

        return await repository.GetByIdAsync(p => p.Id == id);
    }

    public async Task<Client> UpdateAsync(Client client)
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Client>();

        var clientToUpdate = await repository.GetByIdAsync(p => p.Id == client.Id);
        if (clientToUpdate is not null)
        {
            repository.Update(client);
            await unitOfWork.SaveChangesAsync();
            return clientToUpdate;
        }

        return client;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Client>();

        var clientToDelete = await repository.GetByIdAsync(p => p.Id == id);
        if (clientToDelete is not null)
        {
            repository.Delete(clientToDelete);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        return false;
    }
}