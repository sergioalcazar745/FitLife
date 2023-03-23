using FitLife.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.Controllers
{
    public class ClienteController : Controller
    {
        IRepository repo;

        public ClienteController(IRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Calendario()
        {
            return View();
        }

        public IActionResult Hoy()
        {
            return View();
        }

        public IActionResult Semana()
        {
            return View();
        }
    }
}
