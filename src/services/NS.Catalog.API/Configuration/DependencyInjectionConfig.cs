using NS.Catalog.API.Data.Repository;
using NS.Catalog.API.Data;
using NS.Catalog.API.Models;

namespace NS.Catalog.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<CatalogContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
