using CmCapital.Backoffice.Application.Abstractions.MediatR;
using CmCapital.Backoffice.Application.Commands.Response;

namespace CmCapital.Backoffice.Application.Commands.Request
{
    public sealed class BuyProductRequest : ICommand<BuyProductResponse> 
    {
        public required Guid ClientId { get; set; }
        public required Guid ProductId { get; set; }
        public required int Quantity { get; set; }
    }
}