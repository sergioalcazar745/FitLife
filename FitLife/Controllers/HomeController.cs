using Microsoft.AspNetCore.Mvc;
using FitLife.Models;
using FitLife.Repositories;

namespace FitLife.Controllers
{
    public class HomeController : Controller
    {
        IRepository repo;
        public HomeController(IRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(string? error)
        {
            if(error is not null)
            {
                ViewData["Error"] = error;
            }            
            return View();
        }

        [HttpPost]
        public IActionResult Index(Singin signin)
        {
            if(signin.Email == "sergioalcazar745@gmail.com" && signin.Password == "a12345")
            {
                return RedirectToAction("Index", "Client");
            }
            else
            {
                return RedirectToAction("Index", new { error = "El correo electronico o la contraseña son incorrectos" });
            }            
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user, int edad, string sexo, string repeatpassword)
        {
            return RedirectToAction("Index", "Client");
        }
    }
}
