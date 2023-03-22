using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;
using NS.WebApp.MVC.Services;

namespace NS.WebApp.MVC.Controllers;

public class IdentityController : Controller
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

        if (false) return View(newUser);

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

        if (false) return View(login);

        return RedirectToAction("Index", controllerName: "Home");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        return RedirectToAction("Index", controllerName: "Home");
    }
}
