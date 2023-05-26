using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Application.Model;

public sealed class PurchaseModel
{
    public Client Client { get; set; } = default!;
    public Product Product { get; set; } = default!;
    public int Quantity { get; set; }

    public List<string>? Errors { get; set; }

}