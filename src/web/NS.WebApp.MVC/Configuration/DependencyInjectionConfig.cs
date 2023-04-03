using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Services;
using NS.WebApp.MVC.Services.Handlers;

namespace NS.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
                
        services.AddHttpClient<IAuthenticationService, AuthenticationService>();
        
        services.AddHttpClient<ICatalogService, CatalogService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();

    }
}
