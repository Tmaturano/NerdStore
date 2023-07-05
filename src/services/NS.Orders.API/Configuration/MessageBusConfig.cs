using NS.Core.Extensions;
using NS.MessageBus;

namespace NS.Orders.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        //the DI works on singleton for this hosted service
        services.AddMessageBus(configuration.GetMessageQueueConnectionString("MessageBus"));     
    }
}
