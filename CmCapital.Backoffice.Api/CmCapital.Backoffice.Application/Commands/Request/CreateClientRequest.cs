using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Commands.Response;

namespace CmCapital.Backoffice.Application.Commands.Request
{
    public sealed class CreateClientRequest : ICommand<CreateClientResponse>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; } = default!;
        public DateTime LastPurchase { get; set; }
        public required float Balance { get; set; }
    }
}