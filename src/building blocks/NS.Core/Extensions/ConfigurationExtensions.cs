using Microsoft.Extensions.Configuration;

namespace NS.Core.Extensions;

public static class ConfigurationExtensions
{
    public static string GetMessageQueueConnectionString(this IConfiguration configuration, string name) 
        => configuration.GetSection("MessageQueueConnectionString")?[name];
}
