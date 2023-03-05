﻿using FitLife.Models;

namespace FitLife.Repositories
{
    public interface IRepository
    {
        Usuario Login(string email, string password);

        Usuario FindUsuario(int idUsuario);

        Task RegistrarUsuario(string nombre, string apellidos, string dni, string email, string password, string role);

        Task RegistrarCliente(string nombre, string apellidos, string dni, string email, string password, string role, int altura, int peso, int edad, string sexo);
    }
}
