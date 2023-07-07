using Microsoft.Extensions.Options;
using NS.BFF.Shopping.Extensions;
using NS.BFF.Shopping.Services;

namespace NSE.BFF.Compras.Services;

public interface IPaymentService
{
}

public class PaymentService : Service, IPaymentService
{
    private readonly HttpClient _httpClient;

    public PaymentService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.PaymentUrl);
    }
}