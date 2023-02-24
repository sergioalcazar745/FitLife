using Microsoft.AspNetCore.Mvc;

namespace FitLife.Controllers
{
    public class LogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
