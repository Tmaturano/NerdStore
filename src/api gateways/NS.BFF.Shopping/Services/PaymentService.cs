using Microsoft.Extensions.Options;
using NS.Bff.Shopping.Services;
using NS.BFF.Shopping.Extensions;

namespace NSE.Bff.Compras.Services
{
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
}