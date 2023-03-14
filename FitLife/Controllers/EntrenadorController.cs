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
            Usuario usuario = HttpContext.Session.GetObject<Usuario>("user");
            List <UsuarioPerfil> clientes = await this.repo.FindPerfilUsuariosByIdEntrenadorAsync(usuario.IdUsuario);
            return View(clientes);
        }

        public async Task<IActionResult> DetallesCliente(int idcliente)
        {
            UsuarioPerfil usuario = await this.repo.FindClienteAsync(idcliente);
            return View(usuario);
        }
    }
}
