﻿using FitLife.Models;
using System.Drawing;

namespace FitLife.Repositories
{
    public interface IRepository
    {
        Task<Usuario> LoginAsync(string email);

        Task<Usuario> FindUsuarioAsync(int idUsuario);

        Task<Usuario> FindUsuarioByEmailAsync(string email);

        Task<Usuario> FindUsuarioByEmailOrDNIAsync(string email, string dni);

        Task<List<UsuarioIdEmail>> FindUsuariosIdAsync();

        Task<PerfilUsuario> FindPerfilUsuario(int idUsuario);

        Task<int> RegistrarUsuarioAsync(string nombre, string apellidos, string dni, string email, byte[] passwordencrypt, string salt, string password, string role);

        Task<int> RegistrarClienteAsync(string nombre, string apellidos, string dni, string email, byte[] passwordencrypt, string salt, string password, string role, int altura, int peso, int edad, string sexo);

        Task UpdateEstadoUsuarioAsync(int idusuario);

        Task UpdatePasswordAsync(int idusuario, string password, string salt, byte[] passwordencrypt);

        Task RegistrarSolicitudAsync(string salt, int codigo, int idusuario);

        Task<Solicitud> FindSolicitudAsync(int idUsuario);

        Task DeleteSolicitudAsync(int idusuario);

        Task DeleteSolicitudUpdateEstadoUsuarioAsync(int idusuario);

        Task<List<UsuarioPerfil>> FindPerfilUsuariosByIdEntrenadorAsync(int idusuario);

        Task<List<UsuarioPerfil>> FindPerfilUsuariosByIdNutricionistaAsync(int idusuario);

        Task<UsuarioPerfil> FindClienteAsync(int idusuario);

        Task<List<UsuarioPerfil>> GetClientesAsync();

        Task AñadirClienteEntrenadorAsync(int idcliente, int identrenador);

        Task AñadirClienteDietistaAsync(int idcliente, int dietista);

        Task EliminarClienteAsync(int idcliente, int tipo);

        Task<List<Ejercicio>> EjerciciosAsync();

        Task<Rutina> FindRutinaByFechaClienteEntrenadorAsync(string fecha, int identrenador, int idcliente);

        Task<Rutina> FindRutinaByIdAsync(int idrutina);

        Task<int> CrearRutinaAsync(string fecha, string nombre, int idcliente, int identrenador);

        Task<List<RutinaId>> RutinasIdsAsync(int idcliente, int identrenador);

        Task RegistrarEjerciciosRutinaAsync(List<ModelEjercicio> ejercicios, int idrutina);

        Task<List<RutinaDia>> FindRutinaDiaAsync(string fecha, int idcliente, int identrenador);

        Task<List<RutinaId>> FilterRutinaAsync(DateTime fechainicio, DateTime fechafinal, int idcliente, int identrenador);

        Task<List<ModelEjercicio>> EjerciciosRutina(int idrutina);

        Task EliminarRutina(int idrutina);

        Task RegisterComentarioRutinaAsync(string comentario, int idrutina);

        Task<List<Evento>> EventosMesAsync(int idcliente, int identrenador, int mes);

        Task EliminarEjerciciosRutina(List<int> ejercicios);

        Task ModificarEjerciciosRutina(List<ModelEjercicio> ejercicios, int idrutina);

        Task ActualizarRutina(int idrutina, DateTime fecha, string nombre);

        Task<int> AñadirDieta(int idnutricionista, int idcliente, DateTime fecha, string nombre);

        Task<Dieta> GetDieta(int iddieta);

        Task<List<ModelDieta>> DietasId(int idcliente);

        Task<List<Comida>> Comidas();

        Task<List<ComidaAlimento>> DetallesDieta(int iddieta);

        Task<int> GetMaxComidaAlimento();

        Task<int> GetMaxDietas();

        Task<List<Alimento>> Alimentos();

        Task AñadirAlimentosDieta(List<AlimentoAñadir> alimentosAñadir, int iddieta, int idcomida);

        Task<int> CrearComida(int iddieta, string nombre, double totalkcal);

        Task<List<Comida>> GetComidas(int iddieta);

        //Task<List<Alimento>> AlimentosContains(List<AlimentoAñadir> alimentos);

        Task<Alimento> GetAlimento(int idalimento);

        Task<List<ModelDieta>> FilterDietas(DateTime fechainicio, DateTime fechafinal, int idcliente, int idnutricionista);

        Task<Dieta> GetDietaFecha(string fecha, int idcliente, int idnutricionista);

        Task<List<ModelComidaAlimentoNombre>> GetComidaAlimento(int iddieta, int idcomida);

        Task RegisterComentarioDietaAsync(int iddieta, string comentario);

        Task<List<Evento>> EventosMesDietaAsync(int idcliente, int idnutricionista, int mes);

        Task<ComidaAlimento> FindComidaAlimento(int id);

        Task EliminarComidaAlimento(int idcomidalimento);

        Task<double> RestarKcal(double resta, int idcomida);

        Task SumarKcal(double suma, int idcomida);

        Task<Comida> GetComida(int idcomida);

        Task ActualizarComidaAlimento(int idcomialimento, int comida, int alimento, int peso, bool existe);

        Task EliminarComida(int idcomida);

        Task<ComidaAlimento> FindComidaAlimentoByComida(int idcomida);

        Task EliminarDieta(int iddieta);

        Task AñadirComidaAlimento(int iddieta, int idcomida, int idalimento, int peso);

        Task<int> ActualizarDieta(int iddieta, DateTime fecha, string nombre);
    }
}
