using Microsoft.AspNetCore.Mvc;
using FitLife.Models;
using FitLife.Repositories;
using FitLife.Extension;
using System.Text.RegularExpressions;
using FitLife.Helpers;
using Newtonsoft.Json;

namespace FitLife.Controllers
{
    public class HomeController : Controller
    {
        IRepository repo;
        public HomeController(IRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            string error = TempData["ErrorLogin"] as string;
            if (error != null)
            {
                ViewData["Error"] = error;
            }            
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            TempData.Remove("ErrorLogin");
            Usuario usuario = this.repo.Login(email, password);
            if(usuario is not null)
            {
                HttpContext.Session.SetObject("user", usuario);
                return RedirectToAction("Index", "Client");
            }
            else
            {
                TempData["ErrorLogin"] = "El correo electronico o la contraseña son incorrectos";
                return RedirectToAction("Index");
            }            
        }

        public IActionResult RegisterUsuario()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RegisterUsuario(UsuarioValidation usuario)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Usuario usuarioEmail = this.repo.FindUsuarioByEmail(usuario.Email);
            if (usuarioEmail is not null)
            {
                ViewData["ErrorRegister"] = "Parece que el correo ya existe.";
                return View();
            }

            Usuario usuarioDNI = this.repo.FindUsuarioByDNI(usuario.Dni);
            if (usuarioDNI is not null)
            {
                ViewData["ErrorRegister"] = "Parece que el DNI ya existe.";
                return View();
            }

            if (usuario.Role.ToLower() != "cliente")
            {
                await this.repo.RegistrarUsuario(usuario.Nombre, usuario.Apellidos, usuario.Dni, usuario.Email, usuario.Password, usuario.Role);
                return RedirectToAction("Index", "Cliente");
            }

            TempData["Usuario"] = JsonConvert.SerializeObject(usuario);
            return RedirectToAction("RegisterPerfil");
        }

        public async Task<IActionResult> RegisterPerfil()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RegisterPerfil(PerfilUsuario perfil)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            UsuarioValidation usuario = JsonConvert.DeserializeObject<UsuarioValidation>(TempData["Usuario"].ToString());
            await this.repo.RegistrarCliente(usuario.Nombre, usuario.Apellidos, usuario.Dni, usuario.Email,
                usuario.Password, usuario.Role, perfil.Altura, perfil.Peso, perfil.Edad, perfil.Sexo);

            return RedirectToAction("Index", "Cliente");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            return View();
        }
    }
}
