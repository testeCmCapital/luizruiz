using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Interfaces.Services;
using CmCapital.Backoffice.Application.Model;
using CmCapital.Backoffice.Infrastructure.Interfaces.Repository;

namespace CmCapital.Backoffice.Infrastructure.Services;

public class TaxService : ITaxService
{
    private readonly ITaxRepository _taxRepository;
    private readonly IPurchaseService _purchaseService;

    public TaxService(
        ITaxRepository taxRepository,
        IPurchaseService purchaseService)
    {
        _taxRepository = taxRepository ?? throw new ArgumentNullException(nameof(taxRepository));
        _purchaseService = purchaseService ?? throw new ArgumentNullException(nameof(purchaseService));
    }

    public async Task<Result<IEnumerable<SimpleInterestPerYearModel>>> SimpleInterestAsync()
    {
        var taxs = await _taxRepository.GetTaxsAsync();
        var purchases = await _purchaseService.GetPurchasesAsync();

        var query = purchases.Value!
             .Join(taxs, c => c.CreateAt, t => t.CreateAt, (c, t) => new { c, t })
             .GroupBy(x => new { x.c.ProductId,  x.c.Amount, x.t.Percentage })
             .Select(grouped => new SimpleInterestPerYearModel
             {
                    ProductId = grouped.Key.ProductId,
                    Amount = grouped.Key.Amount.ToString(),
                    Percentage = grouped.Key.Percentage.ToString(),
                    Honorarium = grouped.Sum(x => Convert.ToDecimal(x.c.Amount) * x.t.Percentage).ToString()
             });

        return Result<IEnumerable<SimpleInterestPerYearModel>>.Success(query);
    }
}