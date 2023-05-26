namespace CmCapital.Backoffice.Domain.Entities;

public sealed class Purchase : IEntity<Guid>
{
    public Guid ClientId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public float UnitValue { get; set; }
    public float Amount { get; set; }
    public DateTime CreateAt { get; set; }
    public Guid Id { get; set; }
}