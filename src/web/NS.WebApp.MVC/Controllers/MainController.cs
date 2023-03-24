using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Controllers;

public class MainController : Controller
{
    protected bool ResponseHasErrors(ResponseResult response)
    {
        if (response is not null && response.Errors.Messages.Any())
            return true;

        return false;
    }
}
