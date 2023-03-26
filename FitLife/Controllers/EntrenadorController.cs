using FitLife.Extensions;
using FitLife.Filters;
using FitLife.Models;
using FitLife.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections.Generic;
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

        [AuthorizeUsers(Policy = "Profesional")]
        public async Task<IActionResult> Clientes()
        {
            List<UsuarioPerfil> clientes = await this.repo.GetClientesAsync();
            return View(clientes);
        }

        [AuthorizeUsers(Policy = "Profesional")]
        public async Task<IActionResult> AñadirCliente(int idcliente)
        {
            int idUsuario = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            string role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            if(role == "entrenador")
            {
                await this.repo.AñadirClienteEntrenadorAsync(idcliente, idUsuario);
            }else if(role == "nutricionista")
            {
                await this.repo.AñadirClienteDietistaAsync(idcliente, idUsuario);
            }
            return RedirectToAction("Clientes");
        }

        [AuthorizeUsers(Policy = "Profesional")]
        public async Task<IActionResult> DetallesCliente(int idcliente)
        {
            this.memoryCache.Set("idcliente", idcliente);
            UsuarioPerfil usuario = await this.repo.FindClienteAsync(idcliente);
            return View(usuario);
        }

        [AuthorizeUsers]
        public async Task<IActionResult> Perfil()
        {
            int idusuario = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            Usuario usuariomodel = await this.repo.FindUsuarioAsync(idusuario);
            ViewData["USUARIO"] = usuariomodel;
            return View();
        }

        [AuthorizeUsers]
        [HttpPost]
        public async Task<IActionResult> Perfil(UsuarioValidation usuario)
        {
            return View();
        }

        [AuthorizeUsers(Policy = "Profesional")]
        public async Task<IActionResult> EliminarCliente(int idcliente)
        {
            await this.repo.EliminarClienteEntrenadorAsync(idcliente);
            return RedirectToAction("Clientes");
        }

        [AuthorizeUsers(Policy = "Entrenador")]
        public async Task<IActionResult> CrearRutina(int idcliente)
        {
            List<Ejercicio> ejercicios = await this.repo.EjerciciosAsync();
            ViewData["IDCLIENTE"] = idcliente;
            return View(ejercicios);
        }

        [AuthorizeUsers(Policy = "Entrenador")]
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

        [AuthorizeUsers(Policy = "Entrenador")]
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

        [AuthorizeUsers(Policy = "Cliente")]
        public async Task<IActionResult> _RutinaPartial(string fecha)
        {
            int identrenador = int.Parse(HttpContext.User.FindFirstValue("IdEntrenador"));
            int idnutricionista = int.Parse(HttpContext.User.FindFirstValue("IdNutricionista"));
            int idcliente = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());

            ModelCalendario calendario = new ModelCalendario();
            List<RutinaDia> rutina = await this.repo.FindRutinaDiaAsync(fecha, idcliente, identrenador);
            Dieta dieta = await this.repo.GetDietaFecha(fecha, idcliente, idnutricionista);
            calendario.Dieta = dieta;

            if(dieta != null)
            {
                List<Comida> comidas = await this.repo.GetComidas(dieta.IdDieta);
                List<ModelComidaAlimento> modelComidaAlimento = new List<ModelComidaAlimento>();
                foreach (Comida comida in comidas)
                {
                    List<ModelComidaAlimentoNombre> comidaalimento = await this.repo.GetComidaAlimento(dieta.IdDieta, comida.IdComida);
                    ModelComidaAlimento model = new ModelComidaAlimento { Comida = comida, ComidaAlimento = comidaalimento };
                    modelComidaAlimento.Add(model);
                }
                calendario.ComidaAlimentos = modelComidaAlimento;
            }

            if(rutina.Count == 0)
            {
                rutina = null;
            }
            calendario.Rutinas = rutina;
            return PartialView("_RutinaPartial", calendario);
        }

        [AuthorizeUsers(Policy = "Cliente")]
        [HttpPost]
        public async Task<IActionResult> RutinaPartial(int idrutina, string comentario)
        {
            await this.repo.RegisterComentarioRutinaAsync(comentario, idrutina);
            return RedirectToAction("Calendario", "Cliente");
        }

        [AuthorizeUsers(Policy = "Cliente")]
        [HttpPost]
        public async Task<IActionResult> DietaPartial(int iddieta, string comentario)
        {
            await this.repo.RegisterComentarioDietaAsync(iddieta, comentario);
            return RedirectToAction("Calendario", "Cliente");
        }

        [AuthorizeUsers(Policy = "Entrenador")]
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

        [AuthorizeUsers(Policy = "Entrenador")]
        [HttpPost]
        public async Task<IActionResult> Rutinas(DateTime fechainicio, DateTime fechafinal)
        {
            int identrenador = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            int idcliente = int.Parse(this.memoryCache.Get("idcliente").ToString());
            List<RutinaId> rutinas = await this.repo.FilterRutinaAsync(fechainicio, fechafinal, idcliente, identrenador);
            ViewData["FILTRO"] = rutinas.Count + " RESULTADOS";
            return View(rutinas);
        }

        [AuthorizeUsers(Policy = "Entrenador")]
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

        [AuthorizeUsers(Policy = "Entrenador")]
        public async Task<IActionResult> EliminarRutina (int idrutina)
        {
            int idcliente = this.memoryCache.Get<int>("idcliente");
            await this.repo.EliminarRutina(idrutina);
            return RedirectToAction("Rutinas", new { idcliente = idcliente });
        }

        [AuthorizeUsers(Policy = "Cliente")]
        public async Task<IActionResult> EventosMes(int mes, int idcliente)
        {
            int identrenador = int.Parse(HttpContext.User.FindFirstValue("IdEntrenador"));
            int idnutricionista = int.Parse(HttpContext.User.FindFirstValue("IdNutricionista"));
            List<Evento> eventosrutina = await this.repo.EventosMesAsync(idcliente, identrenador, mes);
            List<Evento> eventosdieta = await this.repo.EventosMesDietaAsync(idcliente, idnutricionista, mes);
            ModelEventos modeleventos = new ModelEventos { Dietas = eventosdieta, Rutinas = eventosrutina };
            return Json(modeleventos);
        }
    }
}
