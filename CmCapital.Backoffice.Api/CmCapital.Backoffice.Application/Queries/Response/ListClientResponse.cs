namespace CmCapital.Backoffice.Application.Queries.Response;

public sealed class ListClientResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime LastPurchase { get; set; }
    public float Balance { get; set; }
    public float InitialValue { get; set; }
}