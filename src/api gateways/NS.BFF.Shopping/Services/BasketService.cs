using Microsoft.Extensions.Options;
using NS.BFF.Shopping.Extensions;
using NS.BFF.Shopping.Models;
using NS.Core.Communication;

namespace NS.BFF.Shopping.Services;

public interface IBasketService
{
    Task<BasketDTO> GetBasketAsync();
    Task<ResponseResult> AddItemBasketAsync(BasketItemDTO product);
    Task<ResponseResult> UpdateItemBasketAsync(Guid productId, BasketItemDTO product);
    Task<ResponseResult> RemoveItemBasketAsync(Guid productId);
    Task<ResponseResult> ApplyVoucherBasketAsync(VoucherDTO voucher);
}

public class BasketService : Service, IBasketService
{
    private readonly HttpClient _httpClient;

    public BasketService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.BasketUrl);
    }

    public async Task<ResponseResult> AddItemBasketAsync(BasketItemDTO product)
    {
        var itemContent = GetContent(product);

        var response = await _httpClient.PostAsync("/api/basket/", itemContent);

        if (!HandleResponseErrors(response)) return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }

    public async Task<ResponseResult> ApplyVoucherBasketAsync(VoucherDTO voucher)
    {
        var itemContent = GetContent(voucher);

        var response = await _httpClient.PostAsync("/api/basket/apply-voucher/", itemContent);

        if (!HandleResponseErrors(response)) return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }

    public async Task<BasketDTO> GetBasketAsync()
    {
        var response = await _httpClient.GetAsync("/api/basket/");

        HandleResponseErrors(response);

        return await DeserializeObjectResponse<BasketDTO>(response);
    }

    public async Task<ResponseResult> RemoveItemBasketAsync(Guid productId)
    {
        var response = await _httpClient.DeleteAsync($"/api/basket/{productId}");

        if (!HandleResponseErrors(response)) return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }

    public async Task<ResponseResult> UpdateItemBasketAsync(Guid productId, BasketItemDTO product)
    {
        var itemContent = GetContent(product);

        var response = await _httpClient.PutAsync($"/api/basket/{product.ProductId}", itemContent);

        if (!HandleResponseErrors(response)) return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }
}