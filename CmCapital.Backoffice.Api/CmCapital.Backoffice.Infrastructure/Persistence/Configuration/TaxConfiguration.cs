using CmCapital.Backoffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmCapital.Backoffice.Infrastructure.Persistence.Configuration;

internal sealed class TaxConfiguration : IEntityTypeConfiguration<Tax>
{
    public void Configure(EntityTypeBuilder<Tax> builder) 
    {
        builder
            .ToTable("Taxs");

        builder
            .HasKey(tax => tax.Id);

        builder
            .Property(tax => tax.InitialValue)
            .IsRequired();

        builder
            .Property(tax => tax.FinalValue)
            .IsRequired();

        builder
            .Property(tax => tax.Percentage)
            .IsRequired();

        builder
            .Property(purchase => purchase.CreateAt)
            .HasDefaultValue(DateTime.Now);
    }
}