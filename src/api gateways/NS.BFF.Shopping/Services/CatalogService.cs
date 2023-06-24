using Microsoft.Extensions.Options;
using NS.Bff.Shopping.Services;
using NS.BFF.Shopping.Extensions;

namespace NSE.Bff.Compras.Services
{
    public interface ICatalogService
    {
    }

    public class CataloService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CataloService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);
        }
    }
}