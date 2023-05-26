using CmCapital.Backoffice.Domain.Entities;
using FluentValidation;

namespace CmCapital.Backoffice.Infrastructure.Validators.FluentValidation;

public class ClientValidator : AbstractValidator<Client>
{
    public ClientValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id Obrigatório");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nome Obrigatória")
            .MaximumLength(1000)
            .WithMessage("A descrição não pode ter mais que 1000 caracteres");
    }
}