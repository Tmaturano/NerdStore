using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Services;

namespace NS.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAuthenticationService, AuthenticationService>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();

    }
}
