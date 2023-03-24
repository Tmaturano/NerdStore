using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IAuthenticationService = NS.WebApp.MVC.Services.IAuthenticationService;

namespace NS.WebApp.MVC.Controllers;

public class IdentityController : MainController
{
    private readonly IAuthenticationService _authenticationService;

    public IdentityController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet("new-account")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("new-account")]
    public async Task<IActionResult> Register(UserRegister newUser)
    {
        if (!ModelState.IsValid) return View(newUser);

        var response = await _authenticationService.RegisterAsync(newUser);

        if (ResponseHasErrors(response.ResponseResult)) return View(newUser);

        await DoLoginAsync(response);

        return RedirectToAction("Index", controllerName: "Home");
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLogin login)
    {
        if (!ModelState.IsValid) return View(login);

        var response = await _authenticationService.LoginAsync(login);

        if (ResponseHasErrors(response.ResponseResult)) return View(login);

        await DoLoginAsync(response);

        return RedirectToAction("Index", controllerName: "Home");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", controllerName: "Home");
    }

    private async Task DoLoginAsync(UserLoginResponse response)
    {
        var token = GetFormattedToken(response.AccessToken);

        var claims = new List<Claim>();
        claims.Add(new Claim("JWT", response.AccessToken));
        claims.AddRange(token.Claims);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            IsPersistent = true
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), authProperties);
    }

    private static JwtSecurityToken GetFormattedToken(string jwtToken)
        => new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
}
