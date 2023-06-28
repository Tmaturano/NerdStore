using Microsoft.Extensions.Options;
using NS.Core.Communication;
using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Services;

public interface IShoppingBffService
{
    Task<BasketViewModel> GetBasketAsync();
    Task<int> GetBasketCountAsync();
    Task<ResponseResult> AddItemBasketAsync(BasketItemViewModel product);
    Task<ResponseResult> UpdateItemBasketAsync(Guid productId, BasketItemViewModel product);
    Task<ResponseResult> RemoveItemBasketAsync(Guid productId);
}

public class ShoppingBffService : Service, IShoppingBffService
{
    private readonly HttpClient _httpClient;

    public ShoppingBffService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.ShoppingBffUrl);
    }

    public async Task<ResponseResult> AddItemBasketAsync(BasketItemViewModel product)
    {
        var itemContent = GetContent(product);

        var response = await _httpClient.PostAsync("/api/shopping/basket/items/", itemContent);

        if (!HandleErrorsResponse(response)) return await DeserializeResponseObject<ResponseResult>(response);

        return OkReturn();
    }

    public async Task<BasketViewModel> GetBasketAsync()
    {
        var response = await _httpClient.GetAsync("/api/shopping/basket/");

        HandleErrorsResponse(response);

        return await DeserializeResponseObject<BasketViewModel>(response);
    }

    public async Task<int> GetBasketCountAsync()
    {
        var response = await _httpClient.GetAsync("api/shopping/basket/count");

        HandleErrorsResponse(response);

        return await DeserializeResponseObject<int>(response);
    }

    public async Task<ResponseResult> RemoveItemBasketAsync(Guid productId)
    {
        var response = await _httpClient.DeleteAsync($"/api/shopping/basket/items/{productId}");

        if (!HandleErrorsResponse(response)) return await DeserializeResponseObject<ResponseResult>(response);

        return OkReturn();
    }

    public async Task<ResponseResult> UpdateItemBasketAsync(Guid productId, BasketItemViewModel product)
    {
        var itemContent = GetContent(product);

        var response = await _httpClient.PutAsync($"/api/shopping/basket/items/{product.ProductId}", itemContent);

        if (!HandleErrorsResponse(response)) return await DeserializeResponseObject<ResponseResult>(response);

        return OkReturn();
    }
}
