using Microsoft.Extensions.Options;
using NS.BFF.Shopping.Extensions;

namespace NS.Bff.Shopping.Services
{
    public interface IBasketService
    {
    }

    public class BasketService : Service, IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.BasketUrl);
        }
    }
}