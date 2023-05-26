namespace CmCapital.Backoffice.Domain.Entities;

public sealed class Product : IEntity<Guid>
{
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public DateTime ExpirationDate { get; set; }
    public DateTime RegistrationDate { get; set; }
    public float UnitValue { get; set; }
    public Guid Id { get; set; }
}
