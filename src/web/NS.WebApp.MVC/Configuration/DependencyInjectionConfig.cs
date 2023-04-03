using Microsoft.Extensions.Options;
using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Services;
using NS.WebApp.MVC.Services.Handlers;

namespace NS.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
                
        services.AddHttpClient<IAuthenticationService, AuthenticationService>();

        services.AddHttpClient("Refit", options =>
        {
            options.BaseAddress = new Uri(configuration.GetSection("CatalogUrl").Value);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
          .AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>);

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();

    }
}
