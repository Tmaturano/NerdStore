using NS.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace NS.WebApp.MVC.Services;

public class AuthenticationService : Service, IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<UserLoginResponse> LoginAsync(UserLogin userLogin)
    {
        var loginContent = new StringContent(JsonSerializer.Serialize(userLogin), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://localhost:44326/api/identity/login", loginContent);

        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        if (!HandleErrorsResponse(response))
        {
            return new UserLoginResponse
            {
                ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), serializeOptions)
            };            
        } 

        return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), serializeOptions);
    }

    public async Task<UserLoginResponse> RegisterAsync(UserRegister userRegister)
    {
        var registerContent = new StringContent(JsonSerializer.Serialize(userRegister), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://localhost:44326/api/identity/new-account", registerContent);

        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        if (!HandleErrorsResponse(response))
        {
            return new UserLoginResponse
            {
                ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), serializeOptions)
            };
        }

        return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), serializeOptions);
    }
}
