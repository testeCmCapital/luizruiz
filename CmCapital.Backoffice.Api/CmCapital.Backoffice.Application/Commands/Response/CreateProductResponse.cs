namespace CmCapital.Backoffice.Application.Commands.Response;

public sealed class CreateProductResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; } = default!;
    public DateTime ExpirationDate { get; set; }
    public DateTime RegistrationDate { get; set; }
    public float UnitValue { get; set; }
}