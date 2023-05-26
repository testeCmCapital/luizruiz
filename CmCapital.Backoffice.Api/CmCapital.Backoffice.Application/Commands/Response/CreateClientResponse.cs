namespace CmCapital.Backoffice.Application.Commands.Response;

public sealed class CreateClientResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime LastPurchase { get; set; }
    public float Balance { get; set; }
    public float InitialValue { get; set; }
}