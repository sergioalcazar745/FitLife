using FitLife.Extensions;
using FitLife.Models;
using FitLife.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.Controllers
{
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
            Usuario usuario = HttpContext.Session.GetObject<Usuario>("user");
            await this.repo.AñadirClienteEntrenador(idcliente, usuario.IdUsuario);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DetallesCliente(int idcliente)
        {
            UsuarioPerfil usuario = await this.repo.FindClienteAsync(idcliente);
            return View(usuario);
        }
    }
}
