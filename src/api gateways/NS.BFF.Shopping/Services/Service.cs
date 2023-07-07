using NS.Core.Communication;
using System.Net;
using System.Text;
using System.Text.Json;

namespace NS.BFF.Shopping.Services;

public abstract class Service
{
    protected StringContent GetContent(object data)
    {
        return new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json");
    }

    protected async Task<T> DeserializeObjectResponse<T>(HttpResponseMessage responseMessage)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
    }

    protected bool HandleResponseErrors(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.BadRequest) return false;

        response.EnsureSuccessStatusCode();
        return true;
    }

    protected ResponseResult OkReturn() => new ResponseResult();
}