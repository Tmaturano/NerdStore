using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;
using System.Diagnostics;

namespace NS.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel();

            if (id == 500)
            {
                modelError.Message = "An error occured! Try again later or contact the support.";
                modelError.Title = "An error occured!";
                modelError.ErrorCode = id;
            } else if (id == 404)
            {
                modelError.Message = "The page you are looking for does not exist! <br /> If any questions, contact the support.";
                modelError.Title = "Ouch! Page not found.";
                modelError.ErrorCode = id;
            } else if(id == 403)
            {
                modelError.Message = "You are not allowed to do this.";
                modelError.Title = "Access Denied";
                modelError.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelError);
        }
    }
}