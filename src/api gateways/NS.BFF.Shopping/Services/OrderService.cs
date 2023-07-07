using Microsoft.Extensions.Options;
using NS.BFF.Shopping.Extensions;
using NS.BFF.Shopping.Models;
using NS.Core.Communication;
using System.Net;

namespace NS.BFF.Shopping.Services;

public interface IOrderService
{
    Task<ResponseResult> FinalizeOrderAsync(OrderDTO order);
    Task<OrderDTO> GetLastOrderAsync();
    Task<IEnumerable<OrderDTO>> GetListByClientId();
    Task<VoucherDTO> GetVoucherByCodeAsync(string code);
}

public class OrderService : Service, IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.OrderUrl);
    }

    public async Task<ResponseResult> FinalizeOrderAsync(OrderDTO order)
    {
        var orderContent = GetContent(order);

        var response = await _httpClient.PostAsync("/api/order/", orderContent);

        if (!HandleResponseErrors(response)) return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }

    public async Task<OrderDTO> GetLastOrderAsync()
    {
        var response = await _httpClient.GetAsync("/api/order/last/");

        if (response.StatusCode == HttpStatusCode.NotFound) return null;

        HandleResponseErrors(response);

        return await DeserializeObjectResponse<OrderDTO>(response);
    }

    public async Task<IEnumerable<OrderDTO>> GetListByClientId()
    {
        var response = await _httpClient.GetAsync("/api/order/client-list/");

        if (response.StatusCode == HttpStatusCode.NotFound) return null;

        HandleResponseErrors(response);

        return await DeserializeObjectResponse<IEnumerable<OrderDTO>>(response);
    }

    public async Task<VoucherDTO> GetVoucherByCodeAsync(string code)
    {
        var response = await _httpClient.GetAsync($"/api/voucher/{code}/");

        if (response.StatusCode == HttpStatusCode.NotFound) return null;

        HandleResponseErrors(response);

        return await DeserializeObjectResponse<VoucherDTO>(response);
    }
}