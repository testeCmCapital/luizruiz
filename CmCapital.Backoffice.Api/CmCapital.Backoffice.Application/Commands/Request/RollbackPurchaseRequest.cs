using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Commands.Response;

namespace CmCapital.Backoffice.Application.Commands.Request
{
    public sealed class RollbackPurchaseRequest : ICommand<RollbackPurchaseResponse>
    {
        public required Guid PurchaseId { get; set; }
    }
}