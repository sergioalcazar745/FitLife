using Microsoft.AspNetCore.Mvc;
using FitLife.Models;

namespace FitLife.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Singin signin)
        {
            if(signin.Email == "sergioalcazar745@gmail.com" && signin.Password == "a12345")
            {
                return RedirectToAction("Index", "Log");
            }
            else
            {
                ViewData["Error"] = "El correo electronico o la contraseña son incorrectos";
                return RedirectToAction("Index");
            }
            
        }
    }
}
