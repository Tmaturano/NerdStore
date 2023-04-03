using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Services;
using NS.WebApp.MVC.Services.Handlers;
using Polly;
using Polly.Extensions.Http;

namespace NS.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
                
        services.AddHttpClient<IAuthenticationService, AuthenticationService>();


        var retryWaitPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10),
            }, (outcome, timespan, retryCount, context) =>
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Retrying {retryCount} time(s)");
                Console.ForegroundColor = ConsoleColor.White;
            });

        services.AddHttpClient<ICatalogService, CatalogService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(retryWaitPolicy);
            //.AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));

        //services.AddHttpClient("Refit", options =>
        //{
        //    options.BaseAddress = new Uri(configuration.GetSection("CatalogUrl").Value);
        //}).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
        //  .AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>);

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();

    }
}
