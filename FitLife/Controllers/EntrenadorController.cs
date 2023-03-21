using FitLife.Extensions;
using FitLife.Filters;
using FitLife.Models;
using FitLife.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Security.Claims;

namespace FitLife.Controllers
{
    //[AuthorizeUsers]
    public class EntrenadorController : Controller
    {
        private IRepository repo;
        IMemoryCache memoryCache;
        public EntrenadorController(IRepository repo, IMemoryCache memoryCache)
        {
            this.repo = repo;
            this.memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {
            List <UsuarioPerfil> clientes = await this.repo.GetClientesAsync();
            return View(clientes);
        }

        public async Task<IActionResult> AñadirCliente(int idcliente)
        {
            int idUsuario = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await this.repo.AñadirClienteEntrenadorAsync(idcliente, idUsuario);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DetallesCliente(int idcliente)
        {
            this.memoryCache.Set("idcliente", idcliente);
            UsuarioPerfil usuario = await this.repo.FindClienteAsync(idcliente);
            return View(usuario);
        }

        public IActionResult Perfil()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Perfil(int hola)
        {
            return View();
        }

        public async Task<IActionResult> EliminarCliente(int idcliente)
        {
            await this.repo.EliminarClienteEntrenadorAsync(idcliente);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CrearRutina(int idcliente)
        {
            List<Ejercicio> ejercicios = await this.repo.EjerciciosAsync();
            ViewData["IDCLIENTE"] = idcliente;
            return View(ejercicios);
        }

        [HttpPost]
        public async Task<IActionResult> CrearRutina(ModelRutina rutina)
        {
            int idrutina = await this.repo.CrearRutinaAsync(rutina.Fecha, rutina.Nombre, rutina.IdCliente, int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));           
            if(idrutina == 0)
            {
                return Json("Error");
            }            
            await this.repo.RegistrarEjerciciosRutinaAsync(rutina.Ejercicios, idrutina);
            return Json("Success");
        }

        public async Task<IActionResult> _RutinaPartial(string fecha)
        {
            //meter el idcliente e identrenador
            List<RutinaDia> rutina = await this.repo.FindRutinaDiaAsync(fecha);
            if(rutina.Count == 0)
            {
                rutina = null;
            }
            return PartialView("_RutinaPartial", rutina);
        }

        [HttpPost]
        public async Task<IActionResult> _RutinaPartial(int idrutina, string comentario)
        {
            await this.repo.RegisterComentarioRutinaAsync(comentario, idrutina);
            return RedirectToAction("Index", "Cliente");
        }

        public async Task<IActionResult> Rutinas(int idcliente)
        {
            int identrenador = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<RutinaId> rutinas = await this.repo.RutinasIdsAsync(idcliente, identrenador);
            return View(rutinas);
        }

        [HttpPost]
        public async Task<IActionResult> Rutinas(DateTime fechainicio, DateTime fechafinal)
        {
            return View();
        }

        public async Task<IActionResult> DetallesRutina(int idrutina)
        {
            Rutina rutina = await this.repo.FindRutinaByIdAsync(idrutina);
            List<ModelRutinaEjercicio> ejercicio = await this.repo.EjerciciosRutina(idrutina);
            ViewData["RUTINA"] = rutina;
            return View(ejercicio);
        }

        public async Task<IActionResult> EliminarRutina (int idrutina)
        {
            int idcliente = this.memoryCache.Get<int>("idcliente");
            await this.repo.EliminarRutina(idrutina);
            return RedirectToAction("Rutinas", new { idcliente = idcliente });
        }

        public async Task<IActionResult> EventosMes(int mes, int idcliente)
        {
            int identrenador = int.Parse(HttpContext.User.FindFirstValue("IdEntrenador"));
            List<Evento> eventos = await this.repo.EventosMesAsync(idcliente, identrenador, mes);
            return Json(eventos);
        }
    }
}
