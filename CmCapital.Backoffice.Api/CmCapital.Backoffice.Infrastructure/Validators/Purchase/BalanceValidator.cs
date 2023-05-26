using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Model;
using CmCapital.Backoffice.Domain.Entities;
using CmCapital.Backoffice.Infrastructure.Validators.ChainOfResponsability;

namespace CmCapital.Backoffice.Infrastructure.Validators.Purchase;

public class BalanceValidator : Validator
{
    private List<Product> _products;

    public BalanceValidator(List<Product> products)
    {
        _products = products;
    }

    public override Result<PurchaseWithoutModel?> Validate(PurchaseModel model)
    {
        var PurchaseValue = model!.Product.UnitValue * model.Quantity;

        if (PurchaseValue <= model!.Client.Balance)
        {
            if (_nextValidator != null)
                return _nextValidator.Validate(model);

            return Result<PurchaseWithoutModel?>.Success();
        }

        model.Errors?.Add("você não tem saldo para comprar esse produto");

        var purchaseWithoutMoney = new PurchaseWithoutModel
        {
            Products = _products,
            Errors = model.Errors,
        };

        return Result<PurchaseWithoutModel?>.Conflict(purchaseWithoutMoney);
    }
}