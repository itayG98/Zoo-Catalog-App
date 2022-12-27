using Microsoft.AspNetCore.Mvc;

namespace Zoo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Zoo";
            return View();
        }
    }
}
