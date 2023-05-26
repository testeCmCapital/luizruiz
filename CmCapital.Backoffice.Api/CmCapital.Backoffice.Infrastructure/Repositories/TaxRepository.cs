using CmCapital.Backoffice.Domain.Entities;
using CmCapital.Backoffice.Infrastructure.Interfaces.Repository;
using CmCapital.Backoffice.Infrastructure.Persistence.Context;
using CmCapital.Backoffice.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;

namespace CmCapital.Backoffice.Infrastructure.Repositories;

public sealed class TaxRepository : ITaxRepository
{
    private readonly DbContextOptions<CmCapitalContext> _dbContextOptions;

    public TaxRepository(
        DbContextOptions<CmCapitalContext> options)
    {
        _dbContextOptions = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<IEnumerable<Tax>> GetTaxsAsync()
    {
        using var context = new CmCapitalContext(_dbContextOptions);
        var unitOfWork = new UnitOfWork(context);
        var repository = unitOfWork.GetRepository<Tax>();

        return await repository.GetAllAsync();
    }
}