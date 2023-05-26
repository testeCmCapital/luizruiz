using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Commands.Response;
using CmCapital.Backoffice.Domain.Enums;

namespace CmCapital.Backoffice.Application.Commands.Request
{
    public sealed class CreateProductRequest : ICommand<CreateProductResponse>
    {
        public required Guid Id { get; set; }
        public required string Description { get; set; } = default!;
        public required DateTime ExpirationDate { get; set; }
        public required DateTime RegistrationDate { get; set; }
        public required float UnitValue { get; set; }
        public required Category Category { get; set; } = default!;
    }
}