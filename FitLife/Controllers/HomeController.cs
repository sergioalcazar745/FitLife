using Microsoft.AspNetCore.Mvc;

namespace FitLife.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
