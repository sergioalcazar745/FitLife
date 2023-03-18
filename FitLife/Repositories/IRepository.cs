using FitLife.Models;

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

        Task AñadirClienteEntrenador(int idcliente, int identrenador);

        Task EliminarClienteEntrenador(int idcliente);
    }
}
