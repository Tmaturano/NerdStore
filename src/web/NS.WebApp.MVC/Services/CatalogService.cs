using Microsoft.Extensions.Options;
using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Services;

public class CatalogService : Service, ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient,
        IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);

        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("/api/catalog/products/");

        HandleErrorsResponse(response);

        return await DeserializeResponseObject<IEnumerable<ProductViewModel>>(response);
    }

    public async Task<ProductViewModel> GetByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/catalog/products/{id}");

        HandleErrorsResponse(response);

        return await DeserializeResponseObject<ProductViewModel>(response);
    }
}