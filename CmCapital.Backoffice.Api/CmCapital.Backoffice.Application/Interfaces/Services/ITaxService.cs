using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Model;

namespace CmCapital.Backoffice.Application.Interfaces.Services;

public interface ITaxService 
{
    Task<Result<IEnumerable<SimpleInterestPerYearModel>>> SimpleInterestAsync();
}