using FitLife.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.Controllers
{
    public class ClientController : Controller
    {
        IRepository repo;

        public ClientController(IRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
