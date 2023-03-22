using Microsoft.AspNetCore.Mvc;

namespace NS.WebApp.MVC.Controllers;

public class IdentityController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
