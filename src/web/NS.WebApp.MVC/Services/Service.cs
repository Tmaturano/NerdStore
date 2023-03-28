using NS.WebApp.MVC.Extensions;
using System.Text;
using System.Text.Json;

namespace NS.WebApp.MVC.Services;

public abstract class Service
{
    protected StringContent GetContent(object data) => new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

    protected async Task<T> DeserializeResponseObject<T>(HttpResponseMessage responseMessage)
    {
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), serializeOptions);
    }

    protected bool HandleErrorsResponse(HttpResponseMessage response)
    {
        switch ((int)response.StatusCode)
        {
            case 401:
            case 403:
            case 404:
            case 500:
                throw new CustomHttpResponseException(response.StatusCode);
            case 400:
                return false;
        }

        response.EnsureSuccessStatusCode();
        return true;
    }
}
