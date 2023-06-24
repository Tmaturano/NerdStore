using Microsoft.Extensions.Options;
using NS.Bff.Shopping.Services;
using NS.BFF.Shopping.Extensions;

namespace NSE.Bff.Compras.Services
{
    public interface IOrderService
    {
    }

    public class OrderService : Service, IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.OrderUrl);
        }
    }
}