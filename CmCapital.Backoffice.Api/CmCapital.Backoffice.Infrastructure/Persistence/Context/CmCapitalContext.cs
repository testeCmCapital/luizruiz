using CmCapital.Backoffice.Domain.Entities;
using CmCapital.Backoffice.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CmCapital.Backoffice.Infrastructure.Persistence.Context;

public class CmCapitalContext : DbContext
{
    public CmCapitalContext(DbContextOptions<CmCapitalContext> options)
        : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Tax> Taxs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
        modelBuilder.ApplyConfiguration(new TaxConfiguration());
    }
}