using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Model;
using CmCapital.Backoffice.Infrastructure.Validators.ChainOfResponsability;

namespace CmCapital.Backoffice.Infrastructure.Validators.Purchase;

public class ResidualValidator : Validator
{
    public override Result<PurchaseWithoutModel?> Validate(PurchaseModel model)
    {
        var residualValue = (model.Client.InitialValue * 0.2);
        var spend = model.Client.Balance - residualValue;
        var PurchaseValue = model!.Product.UnitValue * model.Quantity;

        if (PurchaseValue < spend)
        {
            if (_nextValidator != null)
                return _nextValidator.Validate(model);

            return Result<PurchaseWithoutModel?>.Success();
        }

        model.Errors = new List<string>
        {
            "Cliente tem que permanecer com um saldo residual no valor de 20% do saldo inicial"
        };

        return Result<PurchaseWithoutModel?>.BadRequest();
    }
}