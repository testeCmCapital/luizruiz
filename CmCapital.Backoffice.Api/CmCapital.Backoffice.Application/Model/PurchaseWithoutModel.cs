using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Application.Model;

public sealed class PurchaseWithoutModel 
{
    public List<Product> Products { get; set; } = default!;
    public List<string>? Errors { get; set; }
}