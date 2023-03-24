using FitLife.Extensions;
using FitLife.Models;
using FitLife.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace FitLife.Controllers
{
    public class DietistaController : Controller
    {
        RepositorySQL repo;
        IMemoryCache memory;
        public DietistaController(RepositorySQL repo, IMemoryCache memory)
        {
            this.repo = repo;
            this.memory = memory;
        }

        public async Task<IActionResult> Dietas()
        {
            int idcliente = this.memory.Get<int>("idcliente");
            return View();
        }

        public async Task<IActionResult> DetallesDieta(int iddieta)
        {
            int idcliente = this.memory.Get<int>("idcliente");
            List<ModelDieta> dietas = await this.repo.DietasId(idcliente);
            return View();
        }

        public async Task<IActionResult> CrearDieta(DateTime fecha, string nombre)
        {
            int idcliente = this.memory.Get<int>("idcliente");
            List<AlimentoAñadir> alimentos = HttpContext.Session.GetObject<List<AlimentoAñadir>>("Alimentos");
            if(alimentos == null)
            {
                ViewData["MENSAJE"] = "No has introducido ningun alimento";
                return View();
            }

            int idnutricionista = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            int iddieta = await this.repo.AñadirDieta(idnutricionista, idcliente, fecha, nombre);
            await this.repo.AñadirAlimentosDieta(alimentos, iddieta);
            return RedirectToAction("Dietas");
        }

        [HttpPost]
        public IActionResult AñadirAlimento(AlimentoAñadir alimento)
        {
            List<AlimentoAñadir> alimentos;
            if(HttpContext.Session.GetObject<List<AlimentoAñadir>>("Alimentos") == null)
            {
                alimentos = new List<AlimentoAñadir>();
            }
            else
            {
                alimentos = HttpContext.Session.GetObject<List<AlimentoAñadir>>("Alimentos");
            }
            alimentos.Add(alimento);
            HttpContext.Session.SetObject("Alimentos", alimentos);
            return Json("Success");
        }
    }
}
