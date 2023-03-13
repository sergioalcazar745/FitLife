using FitLife.Models;

namespace FitLife.Repositories
{
    public interface IRepository
    {
        Task<Usuario> Login(string email);

        Task<Usuario> FindUsuario(int idUsuario);

        Task<Usuario> FindUsuarioByEmail(string email);

        Task<Usuario> FindUsuarioByEmailOrDNI(string email, string dni);

        Task<PerfilUsuario> FindPerfilUsuario(int idUsuario);

        Task<int> RegistrarUsuario(string nombre, string apellidos, string dni, string email, byte[] passwordencrypt, string salt, string password, string role);

        Task<int> RegistrarCliente(string nombre, string apellidos, string dni, string email, byte[] passwordencrypt, string salt, string password, string role, int altura, int peso, int edad, string sexo);

        Task UpdateEstadoUsuario(int idusuario);

        Task UpdatePassword(int idusuario, string password, string salt, byte[] passwordencrypt);

        Task RegistrarSolicitud(string salt, int codigo, int idusuario);

        Task<Solicitud> FindSolicitud(int idUsuario);

        Task DeleteSolicitud(int idusuario);

        Task DeleteSolicitudUpdateEstadoUsuario(int idusuario);

        Task<List<UsuarioPerfil>> FindPerfilUsuarioByIdProfesional(int idusuario);
    }
}
