using CmCapital.Backoffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmCapital.Backoffice.Infrastructure.Persistence.Configuration;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable("Products");

        builder
            .HasKey(product => product.Id);

        builder
            .Property(product => product.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder
            .Property(product => product.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(product => product.ExpirationDate)
            .IsRequired();

        builder
            .Property(product => product.RegistrationDate)
            .IsRequired();

        builder
            .Property(product => product.UnitValue)
            .IsRequired();
    }
}