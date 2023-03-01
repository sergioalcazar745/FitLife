using Microsoft.AspNetCore.Mvc;

namespace FitLife.Controllers
{
    public class CoachController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
