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

        public async Task<IActionResult> ModificarRutina(RutinaEjerciciosModificar rutina)
        {
            int idcliente = this.memoryCache.Get<int>("idcliente");
            int identrenador = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            Rutina rutinaCheck = await this.repo.FindRutinaByFechaClienteEntrenadorAsync(rutina.Fecha.ToString(), identrenador, idcliente);
            if(rutinaCheck is not null)
            {
                if (rutinaCheck.Fecha != rutina.Fecha)
                {
                    return Json("Error");
                }
            }
            await this.repo.ActualizarRutina(rutina.IdRutina, rutina.Fecha, rutina.Nombre);
            if(rutina.ListaActualizar is not null)
            {
                await this.repo.ModificarEjerciciosRutina(rutina.ListaActualizar, rutina.IdRutina);
            }

            if(rutina.ListaNuevos is not null)
            {
                await this.repo.RegistrarEjerciciosRutinaAsync(rutina.ListaNuevos, rutina.IdRutina);
            }

            if(rutina.ListaEliminar is not null)
            {
                await this.repo.EliminarEjerciciosRutina(rutina.ListaEliminar);
            }
            return Json("Success");
        }

        public async Task<IActionResult> _RutinaPartial(string fecha)
        {
            int identrenador = int.Parse(HttpContext.User.FindFirstValue("IdEntrenador"));
            int idcliente = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            List<RutinaDia> rutina = await this.repo.FindRutinaDiaAsync(fecha, idcliente, identrenador);
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

        public async Task<IActionResult> Rutinas(int idcliente, int? mensaje)
        {
            if(mensaje is not null)
            {
                ViewData["MENSAJE"] = "La rutina ha sido guardado correctamente";
            }
            int identrenador = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<RutinaId> rutinas = await this.repo.RutinasIdsAsync(idcliente, identrenador);
            return View(rutinas);
        }

        [HttpPost]
        public async Task<IActionResult> Rutinas(DateTime fechainicio, DateTime fechafinal)
        {
            int identrenador = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            int idcliente = int.Parse(this.memoryCache.Get("idcliente").ToString());
            List<RutinaId> rutinas = await this.repo.FilterRutinaAsync(fechainicio, fechafinal, idcliente, identrenador);
            ViewData["FILTRO"] = rutinas.Count + " RESULTADOS";
            return View(rutinas);
        }

        public async Task<IActionResult> DetallesRutina(int idrutina, int? mensaje)
        {
            if(mensaje is not null)
            {
                ViewData["MENSAJE"] = "La rutina ha sido actualizada correctamente";
            }
            Rutina rutina = await this.repo.FindRutinaByIdAsync(idrutina);
            List<ModelEjercicio> ejercicios = await this.repo.EjerciciosRutina(idrutina);
            List<Ejercicio> nombreEjercicio = await this.repo.EjerciciosAsync();
            ViewData["RUTINA"] = rutina;
            ViewData["EJERCICIOS"] = nombreEjercicio;
            return View(ejercicios);
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
