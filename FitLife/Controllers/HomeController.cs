using Microsoft.AspNetCore.Mvc;
using FitLife.Models;
using FitLife.Repositories;
using FitLife.Extension;
using System.Text.RegularExpressions;
using FitLife.Helpers;
using Newtonsoft.Json;
using MvcCryptographyBBDD.Helpers;

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
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            Usuario usuario = this.repo.Login(email);
            if (usuario is null)
            {
                ViewData["Error"] = "El correo electronico no existe.";
                return View();
            }
            else
            {
                byte[] passwordencrypt = HelperCryptography.EncryptPassword(password, usuario.Salt);
                if(HelperCryptography.CompareArrays(passwordencrypt, usuario.PasswordEncrypt))
                {
                    HttpContext.Session.SetObject("user", usuario);
                    return RedirectToAction("Index", "Client");
                }
                else
                {
                    ViewData["Error"] = "La contraseña es incorrecta.";
                    return View();
                }
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

            Usuario usuarioValidate = this.repo.FindUsuarioByEmailAndDNI(usuario.Email, usuario.Dni);
            if (usuarioValidate is not null)
            {
                if (usuarioValidate.Email == usuario.Email)
                {
                    ViewData["ErrorRegister"] = "Parece que el correo ya existe.";
                    return View();
                }
                else if (usuarioValidate.Dni == usuario.Dni)
                {
                    ViewData["ErrorRegister"] = "Parece que el DNI ya existe.";
                    return View();
                }
            }

            string salt = HelperCryptography.GenerateSalt();
            byte[] passwordencrypt = HelperCryptography.EncryptPassword(usuario.Password, salt);

            if (usuario.Role.ToLower() != "cliente")
            {
                await this.repo.RegistrarUsuario(usuario.Nombre, usuario.Apellidos, usuario.Dni, usuario.Email, passwordencrypt, salt, usuario.Password, usuario.Role);
                return RedirectToAction("Index", "Cliente");
            }

            TempData["Usuario"] = JsonConvert.SerializeObject(usuario);
            TempData["Salt"] = salt;
            TempData["PasswordEncrypt"] = passwordencrypt;
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
            byte[] passwordencrypt = JsonConvert.DeserializeObject<byte[]>(TempData["PasswordEncrypt"].ToString());
            await this.repo.RegistrarCliente(usuario.Nombre, usuario.Apellidos, usuario.Dni, usuario.Email, passwordencrypt,
                TempData["Salt"] as string, usuario.Password, usuario.Role, perfil.Altura, perfil.Peso, perfil.Edad, perfil.Sexo);

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
