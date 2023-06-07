using Microsoft.Extensions.Options;
using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Services;

public class BasketService : Service, IBasketService
{
    private readonly HttpClient _httpClient;

    public BasketService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.BasketUrl);
    }

    public async Task<ResponseResult> AddItemBasketAsync(ProductItemViewModel product)
    {
        var itemContent = GetContent(product);

        var response = await _httpClient.PostAsync("/basket/", itemContent);

        if (!HandleErrorsResponse(response)) return await DeserializeResponseObject<ResponseResult>(response);

        return OkReturn();
    }

    public async Task<BasketViewModel> GetBasketAsync()
    {
        var response = await _httpClient.GetAsync("/basket/");

        HandleErrorsResponse(response);

        return await DeserializeResponseObject<BasketViewModel>(response);
    }

    public async Task<ResponseResult> RemoveItemBasketAsync(Guid productId)
    {
        var response = await _httpClient.DeleteAsync($"/basket/{productId}");

        if (!HandleErrorsResponse(response)) return await DeserializeResponseObject<ResponseResult>(response);

        return OkReturn();
    }

    public async Task<ResponseResult> UpdateItemBasketAsync(Guid productId, ProductItemViewModel product)
    {
        var itemContent = GetContent(product);

        var response = await _httpClient.PutAsync($"/basket/{product.ProductId}", itemContent);

        if (!HandleErrorsResponse(response)) return await DeserializeResponseObject<ResponseResult>(response);

        return OkReturn();
    }
}
