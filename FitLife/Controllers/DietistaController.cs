using Microsoft.AspNetCore.Mvc;

namespace FitLife.Controllers
{
    public class DietistaController : Controller
    {
        public async Task<IActionResult> Dietas(int idcliente)
        {
            
            return View();
        }

        public IActionResult DetallesDieta(int iddieta)
        {
            return View();
        }

        public IActionResult AñadirDieta(int iddieta)
        {
            return View();
        }
    }
}
