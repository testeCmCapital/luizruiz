using CmCapital.Backoffice.Domain.Entities;

namespace CmCapital.Backoffice.Application.Model;

public sealed class PurchaseOrderModel
{
    public List<Product>? Products { get; set; }

    public Purchase? Purchase { get; set; }
    public Product? Product { get; set; }
}