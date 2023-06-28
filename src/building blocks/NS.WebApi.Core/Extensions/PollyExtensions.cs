using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace NS.WebApi.Core.Extensions;

public static class PollyExtensions
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
            });

        return retryWaitPolicy;
    }
}
