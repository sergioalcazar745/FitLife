﻿using Microsoft.AspNetCore.Mvc;
using FitLife.Models;
using FitLife.Repositories;
using FitLife.Extension;
using System.Text.RegularExpressions;
using FitLife.Helpers;
using Newtonsoft.Json;
using MvcCryptographyBBDD.Helpers;
using Microsoft.Extensions.Caching.Memory;

namespace FitLife.Controllers
{
    public class HomeController : Controller
    {
        IRepository repo;
        IMemoryCache memoryCache;
        public HomeController(IRepository repo, IMemoryCache memoryCache)
        {
            this.repo = repo;
            this.memoryCache = memoryCache;
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
                    return RedirectToAction("Index", "Cliente");
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
                int idusuario = await this.repo.RegistrarUsuario(usuario.Nombre, usuario.Apellidos, usuario.Dni, usuario.Email, passwordencrypt,
                salt, usuario.Password, usuario.Role);

                string saltConfirmacion = HelperCryptography.GenerateSalt();
                await this.repo.RegistrarSolicitud(saltConfirmacion, idusuario);
                this.memoryCache.Set("Token", saltConfirmacion);
                this.memoryCache.Set("Idusuario", idusuario);
                return RedirectToAction("EnviarEmailConfirmacion");
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
            int idusuario = await this.repo.RegistrarCliente(usuario.Nombre, usuario.Apellidos, usuario.Dni, usuario.Email, usuario.PasswordEncrypt,
             usuario.Salt, usuario.Password, usuario.Role, perfil.Altura, perfil.Peso, perfil.Edad, perfil.Sexo);

            this.memoryCache.Remove("Usuario");
            string saltConfirmacion = HelperCryptography.GenerateSalt();
            await this.repo.RegistrarSolicitud(saltConfirmacion, idusuario);
            this.memoryCache.Set("Token", saltConfirmacion);
            this.memoryCache.Set("Idusuario", idusuario);

            return RedirectToAction("EnviarEmailConfirmacion");
        }

        public IActionResult EnviarEmailConfirmacion()
        {
            //Enviar email
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EnviarEmailConfirmacionComprobar()
        {
            int idusuario = this.memoryCache.Get<int>("Idusuario");
            string token = this.memoryCache.Get<string>("Token");
            Solicitud solicitud = this.repo.FindSolicitud(idusuario);

            if(solicitud is not null)
            {
                if(solicitud.Salt == token)
                {
                    this.repo.DeleteSolicitud(solicitud.IdUsuario);
                    //Cambiar el estado del usuario
                    this.memoryCache.Remove("Idusuario");
                    this.memoryCache.Remove("Token");
                    return RedirectToAction("Index", "Cliente");
                }
            }

            return RedirectToAction("EnviarEmailConfirmacion");
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
