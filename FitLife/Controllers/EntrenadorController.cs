using FitLife.Extensions;
using FitLife.Filters;
using FitLife.Models;
using FitLife.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace FitLife.Controllers
{
    //[AuthorizeUsers]
    public class EntrenadorController : Controller
    {
        private IRepository repo;
        public EntrenadorController(IRepository repo)
        {
            this.repo = repo;
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
            int idrutina = await this.repo.CrearRutinaAsync(rutina.Fecha, rutina.IdCliente, int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));           
            await this.repo.RegistrarEjerciciosRutinaAsync(rutina.Ejercicios, idrutina);
            return RedirectToAction("CrearRutina");
        }

        public async Task<IActionResult> _RutinaPartial(string fecha)
        {
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

        public async Task<IActionResult> EventosMes(int mes, int idcliente)
        {
            List<Evento> eventos = await this.repo.EventosMes(idcliente, int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)), mes);
            //string jsonString = JsonConvert.SerializeObject(eventos);
            return Json(eventos);
        }
    }
}
