using FitLife.Models;

namespace FitLife.Repositories
{
    public interface IRepository
    {
        Usuario Login(string email);

        Usuario FindUsuario(int idUsuario);

        Usuario FindUsuarioByEmailAndDNI(string email, string dni);

        PerfilUsuario FindPerfilUsuario(int idUsuario);

        Task<int> RegistrarUsuario(string nombre, string apellidos, string dni, string email, byte[] passwordencrypt, string salt, string password, string role);

        Task<int> RegistrarCliente(string nombre, string apellidos, string dni, string email, byte[] passwordencrypt, string salt, string password, string role, int altura, int peso, int edad, string sexo);

        Task UpdateEstadoUsuario(int idusuario);

        Task RegistrarSolicitud(string salt, int idusuario);

        Solicitud FindSolicitud(int idUsuario);

        Task DeleteSolicitud(int idusuario);
    }
}
