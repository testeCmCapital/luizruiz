using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CmCapital.Backoffice.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAplicationModule(this IServiceCollection services) 
        => services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
}