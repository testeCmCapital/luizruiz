namespace CmCapital.Backoffice.Application.Queries.Response;

public sealed class ListProductResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public DateTime ExpirationDate { get; set; }
    public DateTime RegistrationDate { get; set; }
    public float UnitValue { get; set; }
}