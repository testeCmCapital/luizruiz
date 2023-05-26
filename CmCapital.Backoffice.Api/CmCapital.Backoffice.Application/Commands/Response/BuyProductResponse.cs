namespace CmCapital.Backoffice.Application.Commands.Response;

public sealed class BuyProductResponse 
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid ProductId { get; set; }
    public string Description { get; set; } = default!;
    public float UnitValue { get; set; }
    public float TotalValue { get; set; }
    public int Quantity { get; set; }
}