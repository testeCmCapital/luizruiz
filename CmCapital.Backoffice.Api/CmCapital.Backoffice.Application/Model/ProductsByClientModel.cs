using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Application.Model;

public sealed class ProductsByClientModel
{
    public Guid ClientId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public float Amount { get; set; }
}
