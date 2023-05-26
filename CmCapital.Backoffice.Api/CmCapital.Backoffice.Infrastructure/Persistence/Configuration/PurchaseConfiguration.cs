using CmCapital.Backoffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmCapital.Backoffice.Infrastructure.Persistence.Configuration;

internal sealed class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder
            .ToTable("Purchases");

        builder
            .HasKey(purchase => purchase.Id);

        builder
            .Property(purchase => purchase.ClientId)
            .IsRequired();

        builder
            .Property(purchase => purchase.ProductId)
            .IsRequired();

        builder
            .Property(purchase => purchase.Quantity)
            .IsRequired();

        builder
            .Property(purchase => purchase.UnitValue)
            .IsRequired();

        builder
            .Property(purchase => purchase.Amount)
            .IsRequired();

        builder
            .Property(purchase => purchase.CreateAt)
            .HasDefaultValue(DateTime.Now);
    }
}