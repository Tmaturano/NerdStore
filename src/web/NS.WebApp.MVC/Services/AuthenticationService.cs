using Microsoft.Extensions.Options;
using NS.Core.Communication;
using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Services;

public class AuthenticationService : Service, IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient, IOptions<AppSettings> appSettings)
    {        
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(appSettings.Value.AuthenticationUrl);
    }

    public async Task<UserLoginResponse> LoginAsync(UserLogin userLogin)
    {
        var loginContent = GetContent(userLogin);

        var response = await _httpClient.PostAsync("/api/identity/login", loginContent);

        if (!HandleErrorsResponse(response))
        {
            return new UserLoginResponse
            {
                ResponseResult = await DeserializeResponseObject<ResponseResult>(response)
            };            
        }

        return await DeserializeResponseObject<UserLoginResponse>(response);
    }

    public async Task<UserLoginResponse> RegisterAsync(UserRegister userRegister)
    {
        var registerContent = GetContent(userRegister);

        var response = await _httpClient.PostAsync("/api/identity/new-account", registerContent);

        if (!HandleErrorsResponse(response))
        {
            return new UserLoginResponse
            {
                ResponseResult = await DeserializeResponseObject<ResponseResult>(response)
            };
        }

        return await DeserializeResponseObject<UserLoginResponse>(response);
    }
}
