namespace CmCapital.Backoffice.Domain.Entities;

public sealed class Client : IEntity<Guid>
{
    public string Name { get; set; } = default!;
    public DateTime LastPurchase { get; set; }
    public float Balance { get; set; }
    public float InitialValue { get; set; }
    public Guid Id { get; set; }
}