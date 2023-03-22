using FitLife.Models;
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

        Task EliminarClienteEntrenadorAsync(int idcliente);

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
    }
}
