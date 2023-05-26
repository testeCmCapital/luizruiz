using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Interfaces.Services;
using CmCapital.Backoffice.Application.Queries.Request;
using CmCapital.Backoffice.Application.Queries.Response;

namespace CmCapital.Backoffice.Application.Queries.Handler;

public sealed class GetHonorariumOfYearHandler : IQueryHandler<GetHonorariumOfYearRequest, GetHonorariumOfYearResponse>
{
    private readonly ITaxService _taxService;

    public GetHonorariumOfYearHandler(ITaxService taxService)
    {
        _taxService = taxService ?? throw new ArgumentNullException(nameof(taxService));
    }

    public async Task<Result<GetHonorariumOfYearResponse?>> Handle(
        GetHonorariumOfYearRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _taxService.SimpleInterestAsync();
        if (!result.IsSuccess)
            return Result<GetHonorariumOfYearResponse?>.BadRequest();

        return Result<GetHonorariumOfYearResponse?>.Success(new GetHonorariumOfYearResponse { perYearModels = result.Value! });
    }
}