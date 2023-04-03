using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Services;
using NS.WebApp.MVC.Services.Handlers;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace NS.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
                
        services.AddHttpClient<IAuthenticationService, AuthenticationService>();

        services.AddHttpClient<ICatalogService, CatalogService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(PollyExtensions.WaitAndRetry())
            .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
        /*
         Consecutive exceptions (5) for this case from every call will open the circuit and will need to wait 30 seconds before trying again
        This create resilience in your API, so it may have time to recover for example, an out of memory, or the API is restarting*/

        //services.AddHttpClient("Refit", options =>
        //{
        //    options.BaseAddress = new Uri(configuration.GetSection("CatalogUrl").Value);
        //}).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
        //  .AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>);

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();

    }

    public class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> WaitAndRetry()
        {
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

            return retryWaitPolicy;
        }
    }
}
