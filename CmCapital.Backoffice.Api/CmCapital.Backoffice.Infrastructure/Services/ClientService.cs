using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Interfaces.Services;
using CmCapital.Backoffice.Domain.Entities;
using CmCapital.Backoffice.Infrastructure.Interfaces.Repository;
using CmCapital.Backoffice.Infrastructure.Validators.FluentValidation;
using FluentValidation.Results;

namespace CmCapital.Backoffice.Infrastructure.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
    }

    public async Task<Result<Client?>> CreateClientAsync(Client client)
    {
        var validation = await ValideClientAsync(client);

        if (!validation.IsValid)
            return Result<Client?>.BadRequest(validation.Errors[0].ErrorMessage);

        var created = await _clientRepository.AddAsync(client);

        return created
            ? Result<Client?>.Created(client)
            : Result<Client?>.BadRequest("Não foi possivel cadastrar o client");
    }

    public async Task<Result<IEnumerable<Client?>>> GetClientsAsync()
    {
        var clients = await _clientRepository.GetClientsAsync();

        if (!clients.Any())
            return Result<IEnumerable<Client?>>.Success("Não existe clients");

        return Result<IEnumerable<Client?>>.Success(clients);
    }

    public async Task<Result<Client?>> UpdateClientAsync(Client client)
    {
        var validation = await ValideClientAsync(client);

        if (!validation.IsValid)
            return Result<Client?>.BadRequest(validation.Errors[0].ErrorMessage);

        var updated = await _clientRepository.UpdateAsync(client);

        return Result<Client?>.Success(updated);
    }

    public async Task<Result<bool?>> DeleteClientAsync(Guid id)
    {
        var deleted = await _clientRepository.DeleteAsync(id);

        return deleted
            ? Result<bool?>.Success()
            : Result<bool?>.Conflict("Não foi possivel deletar o client");
    }

    public async Task<Result<Client?>> GetClientAsync(Guid id)
    {
        var client = await _clientRepository.GetClientAsync(id);

        return client is null
            ? Result<Client?>.BadRequest("Não foi possivel encontrar o client")
            : Result<Client?>.Success(client);
    }

    public async Task<Result<bool?>> RollbackBalance(Guid clientId, float amount)
    {
        var client = await GetClientAsync(clientId);
        if (client is null)
            return Result<bool?>.BadRequest();

        client.Value!.Balance += amount;

        var updated = await UpdateClientAsync(client.Value!);

        return updated.IsSuccess
            ? Result<bool?>.Success() 
            : Result<bool?>.Conflict();
    }

    private static async Task<ValidationResult> ValideClientAsync(Client client)
        => await (new ClientValidator()
            .ValidateAsync(client));
}