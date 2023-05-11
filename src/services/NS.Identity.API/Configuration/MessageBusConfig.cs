using NS.Core.Extensions;
using NS.MessageBus;

namespace NS.Identity.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnectionString("MessageBus"));     
    }
}
