using CmCapital.Backoffice.Application.Model;

namespace CmCapital.Backoffice.Application.Queries.Response;

public sealed class GetHonorariumOfYearResponse 
{
    public IEnumerable<SimpleInterestPerYearModel> perYearModels { get; set; } = default!;
}