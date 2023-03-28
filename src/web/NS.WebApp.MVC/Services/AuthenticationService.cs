using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Services;

public class AuthenticationService : Service, IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<UserLoginResponse> LoginAsync(UserLogin userLogin)
    {
        var loginContent = GetContent(userLogin);

        var response = await _httpClient.PostAsync("https://localhost:44326/api/identity/login", loginContent);

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

        var response = await _httpClient.PostAsync("https://localhost:44326/api/identity/new-account", registerContent);

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
