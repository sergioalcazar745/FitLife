using Microsoft.AspNetCore.Mvc;
using FitLife.Models;
using FitLife.Repositories;
using FitLife.Extensions;
using System.Text.RegularExpressions;
using FitLife.Helpers;
using Newtonsoft.Json;
using MvcCryptographyBBDD.Helpers;
using Microsoft.Extensions.Caching.Memory;
using MvcCoreUtilidades.Helpers;
using System;

namespace FitLife.Controllers
{
    public class HomeController : Controller
    {
        IRepository repo;
        IMemoryCache memoryCache;
        HelperMail helperMail;
        public HomeController(IRepository repo, IMemoryCache memoryCache, HelperMail helperMail)
        {
            this.repo = repo;
            this.memoryCache = memoryCache;
            this.helperMail = helperMail;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            Usuario usuario = await this.repo.LoginAsync(email);
            if (usuario is null)
            {
                ViewData["Error"] = "El correo electronico no existe.";
                return View();
            }
            else
            {
                if(usuario.Estado == true)
                {
                    byte[] passwordencrypt = HelperCryptography.EncryptPassword(password, usuario.Salt);
                    if (HelperCryptography.CompareArrays(passwordencrypt, usuario.PasswordEncrypt))
                    {
                        HttpContext.Session.SetObject("user", usuario);
                        HttpContext.Session.GetObject<Usuario>("user");
                        return RedirectToAction("Index", "Cliente");
                    }
                    else
                    {
                        ViewData["Error"] = "La contraseña es incorrecta.";
                        return View();
                    }
                }
                else
                {
                    await this.EnviarConfirmacion(usuario.IdUsuario, usuario.Email);
                    return RedirectToAction("EnviarEmailConfirmacion", new {email = usuario.Email, accion = "register"});
                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index");
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

            Usuario usuarioValidate = await this.repo.FindUsuarioByEmailOrDNIAsync(usuario.Email, usuario.Dni);
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
                int idusuario = await this.repo.RegistrarUsuarioAsync(usuario.Nombre, usuario.Apellidos, usuario.Dni, usuario.Email, passwordencrypt,
                salt, usuario.Password, usuario.Role);

                await this.EnviarConfirmacion(idusuario, usuario.Email);
                return RedirectToAction("EnviarEmailConfirmacion", new {email = usuario.Email, accion = "register"});
            }
            else
            {
                Usuario usuarioEF = new Usuario();
                usuarioEF.Nombre = usuario.Nombre;
                usuarioEF.Apellidos = usuario.Apellidos;
                usuarioEF.Email = usuario.Email;
                usuarioEF.Dni = usuario.Dni;
                usuarioEF.Password = usuario.Password;
                usuarioEF.PasswordEncrypt = passwordencrypt;
                usuarioEF.Salt = salt;
                usuarioEF.Role = usuario.Role;
                this.memoryCache.Set("Usuario", usuarioEF);
                return RedirectToAction("RegisterPerfil");
            }
        }

        public IActionResult RegisterPerfil()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RegisterPerfil(PerfilUsuarioValidation perfil)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Usuario usuario = this.memoryCache.Get<Usuario>("Usuario");
            int idusuario = await this.repo.RegistrarClienteAsync(usuario.Nombre, usuario.Apellidos, usuario.Dni, usuario.Email, usuario.PasswordEncrypt,
             usuario.Salt, usuario.Password, usuario.Role, perfil.Altura, perfil.Peso, perfil.Edad, perfil.Sexo);
            usuario.IdUsuario = idusuario;

            await this.EnviarConfirmacion(idusuario, usuario.Email);
            return RedirectToAction("EnviarEmailConfirmacion", new {email = usuario.Email, accion = "register"});
        }

        public async Task<IActionResult> EnviarEmailConfirmacion(string email, string accion)
        {
            ViewData["Email"] = email;
            TempData["Action"] = accion;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EnviarEmailConfirmacion(int codigo)
        {
            int idusuario = this.memoryCache.Get<int>("IdUsuario");
            string salt = this.memoryCache.Get<string>("Salt");
            Solicitud solicitud = await this.repo.FindSolicitudAsync(idusuario);

            if (solicitud is not null)
            {
                if (solicitud.Salt == salt)
                {
                    if(solicitud.Codigo == codigo)
                    {
                        await this.repo.DeleteSolicitudUpdateEstadoUsuarioAsync(solicitud.IdUsuario);
                        this.memoryCache.Remove("Salt");
                        string action = TempData["Action"] as string;
                        if(action == "password")
                        {
                            return RedirectToAction("NuevaPassword");
                        }
                        else
                        {
                            Usuario usuario = this.memoryCache.Get<Usuario>("Usuario");
                            usuario.IdUsuario = idusuario;
                            HttpContext.Session.SetObject("user", usuario);
                            this.memoryCache.Remove("Usuario");
                            this.memoryCache.Remove("IdUsuario");                            
                            return RedirectToAction("Index", "Cliente");
                        }
                    }
                    else
                    {
                        ViewData["Error"] = "Codigo incorrecto";
                        return View();
                    }
                }
                else
                {
                    ViewData["Error"] = "Parece que no es el mismo dispositivo.";
                    return View();
                }
            }
            else
            {
                ViewData["Error"] = "No hemos encontrado ninguna confirmacion.";
                return View();
            }
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            Usuario usuario = await this.repo.FindUsuarioByEmailAsync(email);
            if (usuario is not null)
            {
                await this.EnviarConfirmacion(usuario.IdUsuario, usuario.Email);
                return RedirectToAction("EnviarEmailConfirmacion", new { email = usuario.Email, accion = "password" });
            }
            else
            {
                ViewData["Error"] = "No existe ese correo";
                return View();
            }
        }

        public IActionResult NuevaPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NuevaPassword(PasswordsValidation passwords)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            int idusuario = this.memoryCache.Get<int>("IdUsuario");
            string salt = HelperCryptography.GenerateSalt();
            byte[] passwordencrypt = HelperCryptography.EncryptPassword(passwords.Password, salt);
            await this.repo.UpdatePasswordAsync(idusuario, passwords.Password, salt, passwordencrypt);

            return RedirectToAction("Index");
        }

        public IActionResult Perfil()
        {
            return View();
        }

        #region METODOS
        public async Task EnviarConfirmacion(int idusuario, string email)
        {
            this.memoryCache.Set("IdUsuario", idusuario);
            string saltConfirmacion = HelperCryptography.GenerateSalt();
            this.memoryCache.Set("Salt", saltConfirmacion);
            Random random = new Random();
            int codigo = random.Next(1000, 15000);
            await this.repo.RegistrarSolicitudAsync(saltConfirmacion, codigo, idusuario);
            string html = "<p style='font-size: 18px'>Hemos recibido un alta en nuestra web. El código de confirmacion es el siguiente<br/></p>" +
                "<h2>"+codigo+"</<h2>";
            await this.helperMail.SendMailAsync(email, "Confirmación", html);
        }
        #endregion
    }
}
