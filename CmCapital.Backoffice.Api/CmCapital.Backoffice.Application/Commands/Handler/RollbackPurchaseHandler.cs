using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Commands.Request;
using CmCapital.Backoffice.Application.Commands.Response;
using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Interfaces.Services;

namespace CmCapital.Backoffice.Application.Commands.Handler;

public sealed class RollbackPurchaseHandler : ICommandHandler<RollbackPurchaseRequest, RollbackPurchaseResponse>
{
    private readonly IPurchaseService _purchaseService;
    private readonly IClientService _clientService;

    public RollbackPurchaseHandler(
        IPurchaseService purchaseService, 
        IClientService clientService)
    {
        _purchaseService = purchaseService ?? throw new ArgumentNullException(nameof(purchaseService));
        _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
    }

    public async Task<Result<RollbackPurchaseResponse?>> Handle(RollbackPurchaseRequest request, CancellationToken cancellationToken)
    {
        //pegar a compra
        var purchase = await _purchaseService.GetPurchaseAsync(request.PurchaseId);
        if(!purchase.IsSuccess)
            return Result<RollbackPurchaseResponse?>.BadRequest("Não foi possivel encontrar a compra");

        //verificar se passou 7 dias
        if(purchase.Value!.CreateAt.AddDays(-7) > DateTime.Now)
            return Result<RollbackPurchaseResponse?>.BadRequest("O prazo de 7 dias para estorno ja passou");

        //deletar a compra
        var deleted = await _purchaseService.DeleteAsync(purchase.Value!.Id);
        if(!deleted.IsSuccess)
            return Result<RollbackPurchaseResponse?>.NotFound("Não conseguimos deletar a compra");

        //ajustar saldo client
        var rollbackBalance = await _clientService.RollbackBalance(purchase.Value!.ClientId, purchase.Value!.Amount);

        return rollbackBalance.IsSuccess
            ? Result<RollbackPurchaseResponse?>.Success()
            : Result<RollbackPurchaseResponse?>.Conflict();
    }
}