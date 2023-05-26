using CmCapital.Backoffice.Application.Core.Http;
using CmCapital.Backoffice.Application.Model;

namespace CmCapital.Backoffice.Infrastructure.Validators.ChainOfResponsability;

public abstract class Validator
{
    protected Validator? _nextValidator;

    public void SetNextValidator(Validator nextValidator)
    {
        _nextValidator = nextValidator;
    }

    public abstract Result<PurchaseWithoutModel?> Validate(PurchaseModel model);
}