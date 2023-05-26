using CmCapital.Backoffice.Domain.Entities;
using FluentValidation;

namespace CmCapital.Backoffice.Infrastructure.Validators.FluentValidation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id Obrigatório");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Descrição Obrigatória")
            .MaximumLength(5000)
            .WithMessage("A descrição não pode ter mais que 5000 caracteres");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Categoria Obrigatória")
            .MaximumLength(50)
            .WithMessage("A categoria não pode ter mais que 50 caracteres");
    }
}