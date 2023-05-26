using CmCapital.Backoffice.Application.DependencyInjection;
using CmCapital.Backoffice.Application.Interfaces.Services;
using CmCapital.Backoffice.Infrastructure.Interfaces.Repository;
using CmCapital.Backoffice.Infrastructure.Options;
using CmCapital.Backoffice.Infrastructure.Persistence.Context;
using CmCapital.Backoffice.Infrastructure.Repositories;
using CmCapital.Backoffice.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CmCapital.Backoffice.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        => services
            .ConfigureOptions(configuration)
            .AddPersistence(configuration)
            .AddAplicationModule()
            .AddServices();
            

    private static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration) 
    {
        services.Configure<ConnectionStringsConfiguration>(cfg => configuration.GetSection(nameof(ConnectionStringsConfiguration)));

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services) 
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IPurchaseService, PurchaseService>();
        services.AddScoped<ITaxService, TaxService>();
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) 
    {
        var connectionString = configuration[$"{nameof(ConnectionStringsConfiguration)}:{nameof(ConnectionStringsConfiguration.DefaultConnection)}"];

        services.AddDbContext<CmCapitalContext>(
            options => options.UseSqlServer(connectionString));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IClientRepository, ClientRepository>();
        services.AddTransient<IPurchaseRepository, PurchaseRepository>();
        services.AddTransient<ITaxRepository, TaxRepository>();
        return services;
    }
}