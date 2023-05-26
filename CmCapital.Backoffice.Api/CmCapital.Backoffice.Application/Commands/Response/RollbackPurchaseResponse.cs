namespace CmCapital.Backoffice.Application.Commands.Response;

public sealed class RollbackPurchaseResponse
{
    public Guid ClientId { get; set; }
    public Guid ProductId { get; set; }
    public float Amount { get; set; }
}