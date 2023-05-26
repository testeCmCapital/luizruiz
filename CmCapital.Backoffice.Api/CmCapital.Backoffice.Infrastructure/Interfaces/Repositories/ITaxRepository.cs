using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Infrastructure.Interfaces.Repository;

public interface ITaxRepository 
{
    Task<IEnumerable<Tax>> GetTaxsAsync();
}