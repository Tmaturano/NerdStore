using NS.Clients.API.Services;
using NS.Core.Extensions;
using NS.MessageBus;

namespace NS.Clients.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        //the DI works on singleton for this hosted service
        services.AddMessageBus(configuration.GetMessageQueueConnectionString("MessageBus"))
            .AddHostedService<AddClientIntegrationHandler>();         
    }
}
