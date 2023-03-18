using FitLife.Extensions;
using FitLife.Filters;
using FitLife.Models;
using FitLife.Repositories;
using Microsoft.AspNetCore.Mvc;
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
            await this.repo.AñadirClienteEntrenador(idcliente, idUsuario);
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
            await this.repo.EliminarClienteEntrenador(idcliente);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CrearRutina()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearRutina(int idcliente)
        {
            return View();
        }
    }
}
