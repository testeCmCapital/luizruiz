using CmCapital.Backoffice.Infrastructure.Interfaces.Repository;
using CmCapital.Backoffice.Infrastructure.Persistence.Context;

namespace CmCapital.Backoffice.Infrastructure.UoW;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
    Task SaveChangesAsync();
}