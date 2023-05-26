using NS.Basket.API.Data;
using NS.WebApi.Core.User;

namespace NS.Basket.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();
        services.AddScoped<BasketContext>();                 
    }
}
