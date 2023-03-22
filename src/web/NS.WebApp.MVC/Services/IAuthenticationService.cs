using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Services;

public interface IAuthenticationService
{
    Task<UserLoginResponse> LoginAsync(UserLogin userLogin);
    Task<UserLoginResponse> RegisterAsync(UserRegister userRegister);
}
