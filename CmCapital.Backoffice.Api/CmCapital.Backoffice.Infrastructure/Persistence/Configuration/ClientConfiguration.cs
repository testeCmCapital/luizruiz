using CmCapital.Backoffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmCapital.Backoffice.Infrastructure.Persistence.Configuration;

internal sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder
            .ToTable("Clients");

        builder
            .HasKey(product => product.Id);

        builder
            .Property(product => product.Name)
            .IsRequired()
            .HasMaxLength(1000);

        builder
            .Property(product => product.LastPurchase)
            .HasDefaultValue(DateTime.MinValue);

        builder
            .Property(product => product.Balance)
            .IsRequired();

        builder
            .Property(product => product.InitialValue)
            .IsRequired();
    }
}