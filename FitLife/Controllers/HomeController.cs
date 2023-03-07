using Microsoft.AspNetCore.Mvc;
using FitLife.Models;
using FitLife.Repositories;
using FitLife.Extension;
using System.Text.RegularExpressions;
using FitLife.Helpers;

namespace FitLife.Controllers
{
    public class HomeController : Controller
    {
        IRepository repo;
        public HomeController(IRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(string? error)
        {
            if(error is not null)
            {
                ViewData["Error"] = error;
            }            
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(Singin signin)
        {
            Usuario usuario = this.repo.Login(signin.Email, signin.Password);
            if(usuario is not null)
            {
                HttpContext.Session.SetObject("user", usuario);
                return RedirectToAction("Index", "Client");
            }
            else
            {
                return RedirectToAction("Index", new { error = "El correo electronico o la contraseña son incorrectos" });
            }            
        }

        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(Usuario user, int edad, int altura, int peso, string sexo, string repeatpassword)
        {
            //List<Error> errores = new List<Error>();
            //Regex rgEmail = new Regex("/^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$/");

            //if (string.IsNullOrEmpty(user.Nombre))
            //{
            //    errores.Add(new Error("nombre", "El nombre es obligatorio"));
            //}

            //if (string.IsNullOrEmpty(user.Apellidos))
            //{
            //    errores.Add(new Error("apellidos", "Los apellidos son obligatorios"));
            //}

            //if (string.IsNullOrEmpty(user.Dni))
            //{
            //    errores.Add(new Error("dni", "El DNI es obligatorio"));
            //}
            //else if (!HelperValidacion.CheckDNI(user.Dni))
            //{
            //    errores.Add(new Error("dni", "Formato incorrecto"));
            //}

            //if (string.IsNullOrEmpty(user.Email))
            //{
            //    errores.Add(new Error("email", "El email es obligatorio"));
            //}
            //else if (rgEmail.IsMatch(user.Email))
            //{
            //    errores.Add(new Error("email", "Formato incorrecto"));
            //}

            //if (string.IsNullOrEmpty(user.Password))
            //{
            //    errores.Add(new Error("password", "La password es obligatoria"));
            //}
            //else
            //{
            //    if(user.Password != repeatpassword)
            //    {
            //        errores.Add(new Error("repeatpassword", "Las contraseñas no coinciden"));
            //    }
            //}

            //if(user.Role.ToLower() == "cliente")
            //{
            //    if (edad == 0)
            //    {
            //        errores.Add(new Error("edad", "La edad es obligatoria"));
            //    }

            //    if (altura == 0)
            //    {
            //        errores.Add(new Error("altura", "La altura es obligatoria"));
            //    }

            //    if (peso == 0)
            //    {
            //        errores.Add(new Error("peso", "El peso es obligatorio"));
            //    }
            //}

            //if (errores.Count == 0)
            //{
            //    if(user.Role.ToLower() == "cliente")
            //    {
            //        await this.repo.RegistrarCliente(user.Nombre, user.Apellidos, user.Dni, user.Email, user.Password, user.Role, altura, peso, edad, sexo);
            //    }
            //    else
            //    {
            //        await this.repo.RegistrarUsuario(user.Nombre, user.Apellidos, user.Dni, user.Email, user.Password, user.Role);
            //    }
            //    return RedirectToAction("Index", "Client");
            //}
            //else
            //{
            //    return View(errores);
            //}
            return View();
        }
    }
}
